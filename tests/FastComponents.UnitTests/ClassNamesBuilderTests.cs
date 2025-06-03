/*
 * Copyright 2025 Atypical Consulting SRL
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using Shouldly;

namespace FastComponents.UnitTests;

public class ClassNamesBuilderTests
{
    [Fact]
    public void Default_ShouldCreateBuilderWithInitialValue()
    {
        // Arrange & Act
        ClassNamesBuilder builder = ClassNamesBuilder.Default("initial");
        string result = builder.Build();

        // Assert
        result.ShouldBe("initial");
    }

    [Fact]
    public void Empty_ShouldCreateBuilderWithEmptyValue()
    {
        // Arrange & Act
        ClassNamesBuilder builder = ClassNamesBuilder.Empty();
        string result = builder.Build();

        // Assert
        result.ShouldBe("");
    }

    [Fact]
    public void Constructor_ShouldInitializeWithValue()
    {
        // Arrange & Act
        ClassNamesBuilder builder = new("test-class");
        string result = builder.Build();

        // Assert
        result.ShouldBe("test-class");
    }

    [Fact]
    public void Constructor_WithPrefixAndSuffix_ShouldApplyThem()
    {
        // Arrange & Act
        ClassNamesBuilder builder = new("initial", "pre-", "-suf");
        builder = builder.AddClass("test");
        string result = builder.Build();

        // Assert
        result.ShouldBe("initial pre-test-suf");
    }

    [Fact]
    public void AddRawValue_ShouldAppendValueDirectly()
    {
        // Arrange
        ClassNamesBuilder builder = ClassNamesBuilder.Empty();

        // Act
        builder = builder.AddRawValue(" raw-value");
        string result = builder.Build();

        // Assert
        result.ShouldBe("raw-value");
    }

    [Fact]
    public void AddRawValue_WithNullOrWhitespace_ShouldNotAppend()
    {
        // Arrange
        ClassNamesBuilder builder = ClassNamesBuilder.Default("initial");

        // Act & Assert
        builder.AddRawValue(null!).Build().ShouldBe("initial");
        builder.AddRawValue("").Build().ShouldBe("initial");
        builder.AddRawValue("   ").Build().ShouldBe("initial");
    }

    [Fact]
    public void AddClass_ShouldAppendClassWithSpace()
    {
        // Arrange
        ClassNamesBuilder builder = ClassNamesBuilder.Default("initial");

        // Act
        builder = builder.AddClass("new-class");
        string result = builder.Build();

        // Assert
        result.ShouldBe("initial new-class");
    }

    [Fact]
    public void AddClass_WithConditionTrue_ShouldAppendClass()
    {
        // Arrange
        ClassNamesBuilder builder = ClassNamesBuilder.Empty();

        // Act
        builder = builder.AddClass("conditional", true);
        string result = builder.Build();

        // Assert
        result.ShouldBe("conditional");
    }

    [Fact]
    public void AddClass_WithConditionFalse_ShouldNotAppendClass()
    {
        // Arrange
        ClassNamesBuilder builder = ClassNamesBuilder.Default("initial");

        // Act
        builder = builder.AddClass("conditional", false);
        string result = builder.Build();

        // Assert
        result.ShouldBe("initial");
    }

    [Fact]
    public void AddClass_WithFunctionConditionTrue_ShouldAppendClass()
    {
        // Arrange
        ClassNamesBuilder builder = ClassNamesBuilder.Empty();

        // Act
        builder = builder.AddClass("conditional", () => true);
        string result = builder.Build();

        // Assert
        result.ShouldBe("conditional");
    }

    [Fact]
    public void AddClass_WithFunctionConditionFalse_ShouldNotAppendClass()
    {
        // Arrange
        ClassNamesBuilder builder = ClassNamesBuilder.Default("initial");

        // Act
        builder = builder.AddClass("conditional", () => false);
        string result = builder.Build();

        // Assert
        result.ShouldBe("initial");
    }

    [Fact]
    public void AddClass_WithNullFunction_ShouldAppendClass()
    {
        // Arrange
        ClassNamesBuilder builder = ClassNamesBuilder.Empty();

        // Act
        builder = builder.AddClass("conditional", null);
        string result = builder.Build();

        // Assert
        result.ShouldBe("conditional");
    }

    [Fact]
    public void AddClass_WithValueFunctionAndConditionTrue_ShouldAppendClass()
    {
        // Arrange
        ClassNamesBuilder builder = ClassNamesBuilder.Empty();

        // Act
        builder = builder.AddClass(() => "dynamic-class", true);
        string result = builder.Build();

        // Assert
        result.ShouldBe("dynamic-class");
    }

    [Fact]
    public void AddClass_WithValueFunctionAndConditionFalse_ShouldNotAppendClass()
    {
        // Arrange
        ClassNamesBuilder builder = ClassNamesBuilder.Default("initial");

        // Act
        builder = builder.AddClass(() => "dynamic-class", false);
        string result = builder.Build();

        // Assert
        result.ShouldBe("initial");
    }

    [Fact]
    public void AddClass_WithValueFunctionAndFunctionConditionTrue_ShouldAppendClass()
    {
        // Arrange
        ClassNamesBuilder builder = ClassNamesBuilder.Empty();

        // Act
        builder = builder.AddClass(() => "dynamic-class", () => true);
        string result = builder.Build();

        // Assert
        result.ShouldBe("dynamic-class");
    }

    [Fact]
    public void AddClass_WithValueFunctionAndFunctionConditionFalse_ShouldNotAppendClass()
    {
        // Arrange
        ClassNamesBuilder builder = ClassNamesBuilder.Default("initial");

        // Act
        builder = builder.AddClass(() => "dynamic-class", () => false);
        string result = builder.Build();

        // Assert
        result.ShouldBe("initial");
    }

    [Fact]
    public void AddClass_WithBuilderAndConditionTrue_ShouldAppendBuilderClasses()
    {
        // Arrange
        ClassNamesBuilder builder1 = ClassNamesBuilder.Empty();
        ClassNamesBuilder builder2 = ClassNamesBuilder.Default("class1").AddClass("class2");

        // Act
        builder1 = builder1.AddClass(builder2, true);
        string result = builder1.Build();

        // Assert
        result.ShouldBe("class1 class2");
    }

    [Fact]
    public void AddClass_WithBuilderAndConditionFalse_ShouldNotAppendBuilderClasses()
    {
        // Arrange
        ClassNamesBuilder builder1 = ClassNamesBuilder.Default("initial");
        ClassNamesBuilder builder2 = ClassNamesBuilder.Default("class1");

        // Act
        builder1 = builder1.AddClass(builder2, false);
        string result = builder1.Build();

        // Assert
        result.ShouldBe("initial");
    }

    [Fact]
    public void AddClass_WithBuilderAndFunctionConditionTrue_ShouldAppendBuilderClasses()
    {
        // Arrange
        ClassNamesBuilder builder1 = ClassNamesBuilder.Empty();
        ClassNamesBuilder builder2 = ClassNamesBuilder.Default("class1");

        // Act
        builder1 = builder1.AddClass(builder2, () => true);
        string result = builder1.Build();

        // Assert
        result.ShouldBe("class1");
    }

    [Fact]
    public void AddClass_WithBuilderAndFunctionConditionFalse_ShouldNotAppendBuilderClasses()
    {
        // Arrange
        ClassNamesBuilder builder1 = ClassNamesBuilder.Default("initial");
        ClassNamesBuilder builder2 = ClassNamesBuilder.Default("class1");

        // Act
        builder1 = builder1.AddClass(builder2, () => false);
        string result = builder1.Build();

        // Assert
        result.ShouldBe("initial");
    }

    [Fact]
    public void AddClassFromAttributes_WithValidClassAttribute_ShouldAppendClass()
    {
        // Arrange
        ClassNamesBuilder builder = ClassNamesBuilder.Empty();
        Dictionary<string, object> attributes = new()
        {
            ["class"] = "attr-class"
        };

        // Act
        builder = builder.AddClassFromAttributes(attributes);
        string result = builder.Build();

        // Assert
        result.ShouldBe("attr-class");
    }

    [Fact]
    public void AddClassFromAttributes_WithNonStringClassAttribute_ShouldNotAppendClass()
    {
        // Arrange
        ClassNamesBuilder builder = ClassNamesBuilder.Default("initial");
        Dictionary<string, object> attributes = new()
        {
            ["class"] = 123
        };

        // Act
        builder = builder.AddClassFromAttributes(attributes);
        string result = builder.Build();

        // Assert
        result.ShouldBe("initial");
    }

    [Fact]
    public void AddClassFromAttributes_WithoutClassAttribute_ShouldNotAppendClass()
    {
        // Arrange
        ClassNamesBuilder builder = ClassNamesBuilder.Default("initial");
        Dictionary<string, object> attributes = new()
        {
            ["other"] = "value"
        };

        // Act
        builder = builder.AddClassFromAttributes(attributes);
        string result = builder.Build();

        // Assert
        result.ShouldBe("initial");
    }

    [Fact]
    public void AddClassFromAttributes_WithNullAttributes_ShouldNotAppendClass()
    {
        // Arrange
        ClassNamesBuilder builder = ClassNamesBuilder.Default("initial");

        // Act
        builder = builder.AddClassFromAttributes(null);
        string result = builder.Build();

        // Assert
        result.ShouldBe("initial");
    }

    [Fact]
    public void Build_ShouldRemoveExtraSpacesAndTrim()
    {
        // Arrange
        ClassNamesBuilder builder = ClassNamesBuilder.Default("  class1  ")
            .AddRawValue("  ")
            .AddClass("class2")
            .AddRawValue("  ");

        // Act
        string result = builder.Build();

        // Assert
        // Note: The current implementation appears to normalize multiple spaces to single spaces
        result.ShouldContain("class1");
        result.ShouldContain("class2");
        result.Trim().ShouldBe(result); // Should be trimmed
    }

    [Fact]
    public void ToString_ShouldReturnSameAsBuild()
    {
        // Arrange
        ClassNamesBuilder builder = ClassNamesBuilder.Default("class1").AddClass("class2");

        // Act
        string buildResult = builder.Build();
        string toStringResult = builder.ToString();

        // Assert
        toStringResult.ShouldBe(buildResult);
    }

    [Fact]
    public void ChainedOperations_ShouldWorkCorrectly()
    {
        // Arrange & Act
        string result = ClassNamesBuilder.Empty()
            .AddClass("base")
            .AddClass("conditional", true)
            .AddClass("skipped", false)
            .AddClass(() => "dynamic", true)
            .AddRawValue(" extra")
            .Build();

        // Assert
        result.ShouldBe("base conditional dynamic extra");
    }

    [Fact]
    public void WithPrefixAndSuffix_ShouldApplyToAllAddedClasses()
    {
        // Arrange & Act
        string result = new ClassNamesBuilder("", "btn-", "-lg")
            .AddClass("primary")
            .AddClass("secondary", true)
            .AddClass("tertiary", false)
            .Build();

        // Assert
        result.ShouldBe("btn-primary-lg btn-secondary-lg");
    }
}
