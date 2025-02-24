using Dfe.Testing.Pages.Public;
using Shouldly;

namespace Dfe.Testing.Pages.UnitTests;
public class PageObjectSchemaMergerTests
{
}
    // TODO refactor these tests - temp placed for confidence over merging schemas

/*    [Fact]
    public void Returns_Empty_When_Seed_IsEmpty_And_Merged_IsEmpty()
    {
        // Arrange
        PageObjectSchemaMerger resultOfMerge = new();

        // Act Assert
        var result = resultOfMerge.Merge([]);

        result.ShouldBeEmpty();
    }


    [Fact]
    public void Returns_Seed_When_Seed_HasValue_And_Merged_IsEmpty()
    {
        // Arrange
        IEnumerable<PageObjectSchema> seed = [
                new PageObjectSchema
                {
                    PageObjectId = "PAGEOBJECT_ID_1",
                    Properties = [
                        new PropertyMapping()
                        {
                            Values = ["text"],
                            OutputProperty = "Property1"
                        }
                    ]
                }
            ];

        // Act
        IEnumerable<PageObjectSchema> mergeSchemas = new PageObjectSchemaMerger(seed).Merge(merge: []);

        // Assert
        Assert.NotNull(mergeSchemas);
        Assert.Equivalent(seed, mergeSchemas);
    }

    // Something in between; PageObjectId doesn't match
    // Something in between ToProperty doesn't match

    [Fact]
    public void Returns_MergedAttribute_When_PageObject_Id_Matches_And_ToProperty_Matches()
    {
        // Arrange
        IEnumerable<PageObjectSchema> seed = [
                new PageObjectSchema
                {
                    PageObjectId = "PAGEOBJECT_ID_1",
                    Properties = [
                        new PropertyMapping()
                        {
                            Values = ["text"],
                            OutputProperty = "Property1"
                        }
                    ]
                }
            ];

        IEnumerable<PageObjectSchema> merge = [
            new PageObjectSchema()
                {
                    PageObjectId = "PAGEOBJECT_ID_1",
                    Properties =
                    [
                        new PropertyMapping()
                        {
                            Values = ["value"],
                            OutputProperty = "Property1"
                        }
                    ]
                }
            ];

        // Act
        IEnumerable<PageObjectSchema> result = new PageObjectSchemaMerger(seed).Merge(merge);

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
        var elem1 = Assert.Single(result);
        elem1.PageObjectId.ShouldBe("PAGEOBJECT_ID_1");
        elem1.Properties.ShouldNotBeNull();
        var mapping = elem1.Properties.ToList()[0];
        mapping.Values.ShouldBe(["value"]);
        mapping.OutputProperty.ShouldBe("Property1");
    }

    [Fact]
    public void Overide_PageObjectSchemaId_Does_Not_Match_Merges_Mapping()
    {
        // Arrange
        IEnumerable<PageObjectSchema> seed = [
            new PageObjectSchema
                    {
                        PageObjectId = "PAGEOBJECT_ID_1",
                        Properties = [
                            new PropertyMapping()
                            {
                                Values = ["text"],
                                OutputProperty = "Property1"
                            }
                        ]
                    }
        ];

        IEnumerable<PageObjectSchema> overrideSchema = [
            new PageObjectSchema()
                {
                    PageObjectId = "PAGEOBJECT_ID_2",
                    Properties =
                    [
                        new PropertyMapping()
                        {
                            Values = ["value"],
                            OutputProperty = "Property1"
                        }
                    ],
                    Children = []
                }
            ];

        // Act
        IEnumerable<PageObjectSchema> result = new PageObjectSchemaMerger(seed).Merge(overrideSchema);

        // Assert
        Assert.NotNull(result);
        Assert.Collection(result,
            elem1 =>
            {
                Assert.Equal("PAGEOBJECT_ID_1", elem1.PageObjectId);
                var mappings = elem1.Properties.ToList();
                Assert.Equal(["text"], mappings[0].Values);
                Assert.Equal("Property1", mappings[0].OutputProperty);
            },
            elem2 =>
            {
                Assert.Equal("PAGEOBJECT_ID_2", elem2.PageObjectId);
                var mappings = elem2.Properties.ToList();
                Assert.Equal(["value"], mappings[0].Values);
                Assert.Equal("Property1", mappings[0].OutputProperty);
            });
    }

    [Fact]
    public void Overide_APageObjectSchemaId_Matches_Merges_MappedOverlap_FavouringRequest()
    {
        // Arrange
        IEnumerable<PageObjectSchema> seed = [
            new PageObjectSchema
                    {
                        PageObjectId = "PAGEOBJECT_ID_1",
                        Properties = [
                            new PropertyMapping()
                            {
                                Values = ["text"],
                                OutputProperty = "MyOutputProperty1"
                            }
                        ]
                    }
        ];

        IEnumerable<PageObjectSchema> overrideSchema = [
            new PageObjectSchema()
                {
                    PageObjectId = "PAGEOBJECT_ID_2",
                    Properties =
                    [
                        new PropertyMapping()
                        {
                            Values = ["value"],
                            OutputProperty = "MyOutputProperty2"
                        }
                    ],
                    Children = []
                },
            new PageObjectSchema()
                {
                    PageObjectId = "PAGEOBJECT_ID_1",
                    Properties =
                    [
                        new PropertyMapping()
                        {
                            Values = ["ATTRIBUTE1", "ATTRIBUTE@2", "1ATTRIBUTE%3"],
                            OutputProperty = "MyOutputProperty1"
                        },
                        new PropertyMapping()
                        {
                            Values = ["else", "a", "£$%£$^S"],
                            OutputProperty = "NotOverwrittenProperty3"
                        }
                    ],
                    Children = []
                }
            ];

        // Act

        List<PageObjectSchema> result = new PageObjectSchemaMerger(seed).Merge(overrideSchema).ToList();

        // Assert

        Assert.NotNull(result);
        Assert.Collection(result,
            elem1 =>
            {
                Assert.Equal("PAGEOBJECT_ID_1", elem1.PageObjectId);
                var resultMappings = elem1.Properties.ToList();
                Assert.Equal(["ATTRIBUTE1", "ATTRIBUTE@2", "1ATTRIBUTE%3"], resultMappings[0].Values);
                Assert.Equal("MyOutputProperty1", resultMappings[0].OutputProperty);

                var firstMappedAttribute = elem1.Properties.ToList();
                Assert.Equal(["else", "a", "£$%£$^S"], resultMappings[1].Values);
                Assert.Equal("NotOverwrittenProperty3", resultMappings[1].OutputProperty);
            },
            elem2 =>
            {
                Assert.Equal("PAGEOBJECT_ID_2", elem2.PageObjectId);
                var firstMappedAttribute = elem2.Properties.ToList();
                Assert.Equal(["value"], firstMappedAttribute[0].Values);
                Assert.Equal("MyOutputProperty2", firstMappedAttribute[0].OutputProperty);
            });
    }
}
*/
