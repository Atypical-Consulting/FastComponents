using Shouldly;

namespace FastComponents.UnitTests;

public class GenerateParameterMethodsAttributeTests
{
    [Fact]
    public void GenerateParameterMethodsAttribute_DefaultValues_SkipDefaultsIsTrue()
    {
        // Act
        GenerateParameterMethodsAttribute attribute = new();

        // Assert
        attribute.SkipDefaults.ShouldBe(true);
    }

    [Fact]
    public void GenerateParameterMethodsAttribute_CanSetSkipDefaults()
    {
        // Act
        GenerateParameterMethodsAttribute attribute = new()
        {
            SkipDefaults = false
        };

        // Assert
        attribute.SkipDefaults.ShouldBe(false);
    }

    [Fact]
    public void GenerateParameterMethodsAttribute_CanBeAppliedToClass()
    {
        // Arrange
        Type type = typeof(TestClassWithAttribute);

        // Act
        object[] attributes = type.GetCustomAttributes(typeof(GenerateParameterMethodsAttribute), false);

        // Assert
        attributes.Length.ShouldBe(1);
        GenerateParameterMethodsAttribute? attribute = attributes[0] as GenerateParameterMethodsAttribute;
        attribute.ShouldNotBeNull();
    }

    [Fact]
    public void GenerateParameterMethodsAttribute_IsNotInheritable()
    {
        // Arrange
        Type baseType = typeof(TestClassWithAttribute);
        Type derivedType = typeof(DerivedTestClass);

        // Act
        object[] baseAttributes = baseType.GetCustomAttributes(typeof(GenerateParameterMethodsAttribute), false);
        object[] derivedAttributes = derivedType.GetCustomAttributes(typeof(GenerateParameterMethodsAttribute), false);

        // Assert
        baseAttributes.Length.ShouldBe(1);
        derivedAttributes.Length.ShouldBe(0);
    }

    [Fact]
    public void GenerateParameterMethodsAttribute_AttributeUsage_IsCorrect()
    {
        // Arrange
        Type attributeType = typeof(GenerateParameterMethodsAttribute);

        // Act
        object[] usageAttributes = attributeType.GetCustomAttributes(typeof(AttributeUsageAttribute), false);

        // Assert
        usageAttributes.Length.ShouldBe(1);
        AttributeUsageAttribute? usage = usageAttributes[0] as AttributeUsageAttribute;
        usage.ShouldNotBeNull();
        usage.ValidOn.ShouldBe(AttributeTargets.Class);
        usage.Inherited.ShouldBe(false);
        usage.AllowMultiple.ShouldBe(false);
    }

    [GenerateParameterMethods]
    private class TestClassWithAttribute
    {
    }

    private class DerivedTestClass : TestClassWithAttribute
    {
    }
}
