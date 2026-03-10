#!/bin/bash

# FastComponents Code Coverage Script
# Generates code coverage reports using Microsoft Testing Platform and ReportGenerator

set -e

echo "🧪 Running tests with code coverage..."

# Clean previous coverage results
rm -rf CoverageReport
rm -f coverage.cobertura.xml

# Run tests with coverage for all test projects
dotnet test --configuration Release -- --coverage --coverage-output-format cobertura --coverage-output coverage.cobertura.xml

echo "📊 Generating HTML coverage report..."

# Find the generated coverage file (it may be in different locations)
COVERAGE_FILE=""
if [ -f "tests/FastComponents.UnitTests/bin/Release/net9.0/TestResults/coverage.cobertura.xml" ]; then
    COVERAGE_FILE="tests/FastComponents.UnitTests/bin/Release/net9.0/TestResults/coverage.cobertura.xml"
elif [ -f "tests/FastComponents.UnitTests/bin/Debug/net9.0/TestResults/coverage.cobertura.xml" ]; then
    COVERAGE_FILE="tests/FastComponents.UnitTests/bin/Debug/net9.0/TestResults/coverage.cobertura.xml"
else
    echo "❌ Coverage file not found. Looking for available files..."
    find . -name "coverage.cobertura.xml" -type f
    exit 1
fi

echo "📄 Using coverage file: $COVERAGE_FILE"

# Generate HTML report
reportgenerator \
    -reports:"$COVERAGE_FILE" \
    -targetdir:CoverageReport \
    -reporttypes:Html

echo "✅ Coverage report generated successfully!"

# Display summary
if command -v reportgenerator &> /dev/null; then
    echo ""
    echo "📈 Coverage Summary:"
    reportgenerator \
        -reports:"$COVERAGE_FILE" \
        -targetdir:temp_summary \
        -reporttypes:TextSummary
    cat temp_summary/Summary.txt 2>/dev/null || echo "Summary not available"
    rm -rf temp_summary
fi

# Ask user if they want to open the report
echo ""
read -p "🌐 Would you like to open the coverage report in your browser? (y/n) " -n 1 -r
echo ""

if [[ $REPLY =~ ^[Yy]$ ]]; then
    # Detect OS and open the appropriate browser
    if [[ "$OSTYPE" == "darwin"* ]]; then
        # macOS
        open CoverageReport/index.html
    elif [[ "$OSTYPE" == "linux-gnu"* ]]; then
        # Linux
        if command -v xdg-open &> /dev/null; then
            xdg-open CoverageReport/index.html
        else
            echo "❌ Could not detect browser opener. Please open CoverageReport/index.html manually."
        fi
    elif [[ "$OSTYPE" == "msys" || "$OSTYPE" == "cygwin" || "$OSTYPE" == "win32" ]]; then
        # Windows
        start CoverageReport/index.html
    else
        echo "❌ Unknown OS type. Please open CoverageReport/index.html manually."
    fi
else
    echo "📁 You can open CoverageReport/index.html manually to view the report."
fi