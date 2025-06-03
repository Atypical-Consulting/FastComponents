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

public class HxHelpersTests
{
    [Fact]
    public void Swap_InnerHtml_ShouldHaveCorrectValue()
    {
        // Assert
        Hx.Swap.InnerHtml.ShouldBe("innerHTML");
    }

    [Fact]
    public void Swap_OuterHtml_ShouldHaveCorrectValue()
    {
        // Assert
        Hx.Swap.OuterHtml.ShouldBe("outerHTML");
    }

    [Fact]
    public void Swap_BeforeBegin_ShouldHaveCorrectValue()
    {
        // Assert
        Hx.Swap.BeforeBegin.ShouldBe("beforebegin");
    }

    [Fact]
    public void Swap_AfterBegin_ShouldHaveCorrectValue()
    {
        // Assert
        Hx.Swap.AfterBegin.ShouldBe("afterbegin");
    }

    [Fact]
    public void Swap_BeforeEnd_ShouldHaveCorrectValue()
    {
        // Assert
        Hx.Swap.BeforeEnd.ShouldBe("beforeend");
    }

    [Fact]
    public void Swap_AfterEnd_ShouldHaveCorrectValue()
    {
        // Assert
        Hx.Swap.AfterEnd.ShouldBe("afterend");
    }

    [Fact]
    public void Swap_Delete_ShouldHaveCorrectValue()
    {
        // Assert
        Hx.Swap.Delete.ShouldBe("delete");
    }

    [Fact]
    public void Swap_None_ShouldHaveCorrectValue()
    {
        // Assert
        Hx.Swap.None.ShouldBe("none");
    }

    [Theory]
    [InlineData("myId", "#myId")]
    [InlineData("test-123", "#test-123")]
    [InlineData("", "#")]
    [InlineData("element", "#element")]
    public void TargetId_ShouldPrependHashSymbol(string id, string expected)
    {
        // Act
        string result = Hx.TargetId(id);

        // Assert
        result.ShouldBe(expected);
    }

    [Fact]
    public void TargetId_WithNullInput_ShouldReturnHashWithNull()
    {
        // Act
        string result = Hx.TargetId(null!);

        // Assert
        result.ShouldBe("#");
    }
}
