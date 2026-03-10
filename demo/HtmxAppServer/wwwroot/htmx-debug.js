// HTMX Debug Monitor - Detects recursive requests and performance issues
(function() {
    'use strict';
    
    const debugConfig = {
        enabled: true,
        logLevel: 'warn', // 'debug', 'info', 'warn', 'error'
        maxRequestsPerTarget: 3,
        maxRequestsPerSecond: 10,
        slowRequestThreshold: 2000, // ms
        enableConsoleGrouping: true
    };

    const requestTracking = {
        activeRequests: new Map(),
        targetCounts: new Map(),
        recentRequests: [],
        requestCount: 0
    };

    function log(level, message, ...args) {
        if (!debugConfig.enabled) return;
        
        const levels = ['debug', 'info', 'warn', 'error'];
        const configLevel = levels.indexOf(debugConfig.logLevel);
        const messageLevel = levels.indexOf(level);
        
        if (messageLevel >= configLevel) {
            console[level](`[HTMX Debug] ${message}`, ...args);
        }
    }

    function startGroup(title) {
        if (debugConfig.enableConsoleGrouping) {
            console.group(`[HTMX Debug] ${title}`);
        }
    }

    function endGroup() {
        if (debugConfig.enableConsoleGrouping) {
            console.groupEnd();
        }
    }

    function trackRequestStart(detail) {
        const { xhr, target, pathInfo } = detail;
        const requestId = ++requestTracking.requestCount;
        const targetId = target.id || target.tagName + (target.className ? '.' + target.className.split(' ')[0] : '');
        const now = Date.now();

        // Track active request
        requestTracking.activeRequests.set(xhr, {
            id: requestId,
            startTime: now,
            target: targetId,
            url: pathInfo.requestPath,
            element: target
        });

        // Update target counts
        const currentCount = requestTracking.targetCounts.get(targetId) || 0;
        requestTracking.targetCounts.set(targetId, currentCount + 1);

        // Add to recent requests (for rate limiting detection)
        requestTracking.recentRequests.push({ time: now, target: targetId, url: pathInfo.requestPath });
        
        // Clean old recent requests (older than 1 second)
        requestTracking.recentRequests = requestTracking.recentRequests.filter(req => now - req.time < 1000);

        // Check for issues
        checkForRecursiveRequests(targetId, currentCount + 1);
        checkForRapidRequests();

        log('debug', `Request started: #${requestId} to ${targetId} (${pathInfo.requestPath})`);
    }

    function trackRequestEnd(detail) {
        const { xhr } = detail;
        const request = requestTracking.activeRequests.get(xhr);
        
        if (request) {
            const duration = Date.now() - request.startTime;
            const targetCount = requestTracking.targetCounts.get(request.target) || 0;
            
            // Update target count
            requestTracking.targetCounts.set(request.target, Math.max(0, targetCount - 1));
            
            // Remove from active requests
            requestTracking.activeRequests.delete(xhr);

            // Check for slow requests
            if (duration > debugConfig.slowRequestThreshold) {
                log('warn', `Slow request detected: #${request.id} took ${duration}ms`, request);
            }

            log('debug', `Request completed: #${request.id} in ${duration}ms`);
        }
    }

    function checkForRecursiveRequests(targetId, count) {
        if (count > debugConfig.maxRequestsPerTarget) {
            startGroup(`🔄 RECURSIVE REQUESTS DETECTED`);
            log('error', `Target "${targetId}" has ${count} concurrent requests!`);
            log('warn', 'This usually indicates:');
            log('warn', '• hx-trigger="load" on self-replacing elements');
            log('warn', '• Event handlers triggering the same request');
            log('warn', '• Incorrect hx-target pointing to parent of trigger');
            
            // Find all active requests for this target
            const targetRequests = Array.from(requestTracking.activeRequests.values())
                .filter(req => req.target === targetId);
                
            if (targetRequests.length > 0) {
                log('warn', 'Active requests for this target:', targetRequests);
            }
            endGroup();
        }
    }

    function checkForRapidRequests() {
        const now = Date.now();
        const recentCount = requestTracking.recentRequests.length;
        
        if (recentCount > debugConfig.maxRequestsPerSecond) {
            startGroup(`⚡ RAPID REQUESTS DETECTED`);
            log('error', `${recentCount} requests in the last second!`);
            
            // Group by target
            const byTarget = requestTracking.recentRequests.reduce((acc, req) => {
                acc[req.target] = (acc[req.target] || 0) + 1;
                return acc;
            }, {});
            
            log('warn', 'Requests by target:', byTarget);
            endGroup();
        }
    }

    function checkForPotentialIssues(detail) {
        const { target, elt } = detail;
        
        // Check for load trigger on self-replacing elements
        const trigger = elt.getAttribute('hx-trigger');
        const swapMode = elt.getAttribute('hx-swap');
        const targetAttr = elt.getAttribute('hx-target');
        
        if (trigger && trigger.includes('load') && !trigger.includes('once')) {
            if (swapMode === 'outerHTML' || (!targetAttr && swapMode !== 'innerHTML')) {
                log('warn', '⚠️ Potential infinite loop detected:', {
                    element: elt,
                    issue: 'hx-trigger="load" without "once" on self-replacing element',
                    suggestion: 'Use hx-trigger="load once" or ensure hx-target points to a different element'
                });
            }
        }

        // Check for event triggers that might cause recursion
        if (trigger && (trigger.includes('click') || trigger.includes('change'))) {
            const targetElement = targetAttr ? document.querySelector(targetAttr) : elt;
            if (targetElement && (targetElement === elt || targetElement.contains(elt))) {
                log('warn', '⚠️ Potential recursion detected:', {
                    element: elt,
                    issue: 'Event trigger targets element that contains the trigger',
                    suggestion: 'Ensure hx-target points to a different element or use hx-disable during request'
                });
            }
        }
    }

    // Set up HTMX event listeners
    if (typeof htmx !== 'undefined') {
        // Request lifecycle tracking
        document.addEventListener('htmx:beforeRequest', function(event) {
            trackRequestStart(event.detail);
            checkForPotentialIssues(event.detail);
        });

        document.addEventListener('htmx:afterRequest', function(event) {
            trackRequestEnd(event.detail);
        });

        // Error tracking
        document.addEventListener('htmx:responseError', function(event) {
            log('error', 'HTMX Response Error:', event.detail);
        });

        document.addEventListener('htmx:sendError', function(event) {
            log('error', 'HTMX Send Error:', event.detail);
        });

        // Performance monitoring
        document.addEventListener('htmx:beforeSwap', function(event) {
            const request = Array.from(requestTracking.activeRequests.values())
                .find(req => req.element === event.detail.target);
                
            if (request) {
                const duration = Date.now() - request.startTime;
                if (duration > debugConfig.slowRequestThreshold) {
                    log('warn', `Slow swap detected: ${duration}ms for request #${request.id}`);
                }
            }
        });

        // Expose debug API globally
        window.htmxDebug = {
            getActiveRequests: () => Array.from(requestTracking.activeRequests.values()),
            getTargetCounts: () => Object.fromEntries(requestTracking.targetCounts),
            getRecentRequests: () => requestTracking.recentRequests.slice(),
            enable: () => debugConfig.enabled = true,
            disable: () => debugConfig.enabled = false,
            setLogLevel: (level) => debugConfig.logLevel = level,
            clearTracking: () => {
                requestTracking.activeRequests.clear();
                requestTracking.targetCounts.clear();
                requestTracking.recentRequests = [];
                requestTracking.requestCount = 0;
            }
        };

        log('info', 'HTMX Debug Monitor initialized. Use window.htmxDebug for manual inspection.');
        log('info', 'Debug configuration:', debugConfig);
    } else {
        console.warn('[HTMX Debug] HTMX library not found. Debug monitor disabled.');
    }
})();