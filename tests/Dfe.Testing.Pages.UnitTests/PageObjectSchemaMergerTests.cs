using Dfe.Testing.Pages.Public;
using Shouldly;

namespace Dfe.Testing.Pages.UnitTests;
public class PageObjectSchemaMergerTests
{
    // TODO refactor these tests - temp placed for confidence over merging schemas

    [Fact]
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
                    Id = "PAGEOBJECT_ID_1",
                    Properties = [
                        new PropertyMapping()
                        {
                            Attributes = ["text"],
                            ToProperty = "Property1"
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
                    Id = "PAGEOBJECT_ID_1",
                    Properties = [
                        new PropertyMapping()
                        {
                            Attributes = ["text"],
                            ToProperty = "Property1"
                        }
                    ]
                }
            ];

        IEnumerable<PageObjectSchema> merge = [
            new PageObjectSchema()
                {
                    Id = "PAGEOBJECT_ID_1",
                    Properties =
                    [
                        new PropertyMapping()
                        {
                            Attributes = ["value"],
                            ToProperty = "Property1"
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
        elem1.Id.ShouldBe("PAGEOBJECT_ID_1");
        elem1.Properties.ShouldNotBeNull();
        var mapping = elem1.Properties.ToList()[0];
        mapping.Attributes.ShouldBe(["value"]);
        mapping.ToProperty.ShouldBe("Property1");
    }

    [Fact]
    public void Overide_PageObjectSchemaId_Does_Not_Match_Merges_Mapping()
    {
        // Arrange
        IEnumerable<PageObjectSchema> seed = [
            new PageObjectSchema
                    {
                        Id = "PAGEOBJECT_ID_1",
                        Properties = [
                            new PropertyMapping()
                            {
                                Attributes = ["text"],
                                ToProperty = "Property1"
                            }
                        ]
                    }
        ];

        IEnumerable<PageObjectSchema> overrideSchema = [
            new PageObjectSchema()
                {
                    Id = "PAGEOBJECT_ID_2",
                    Properties =
                    [
                        new PropertyMapping()
                        {
                            Attributes = ["value"],
                            ToProperty = "Property1"
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
                Assert.Equal("PAGEOBJECT_ID_1", elem1.Id);
                var mappings = elem1.Properties.ToList();
                Assert.Equal(["text"], mappings[0].Attributes);
                Assert.Equal("Property1", mappings[0].ToProperty);
            },
            elem2 =>
            {
                Assert.Equal("PAGEOBJECT_ID_2", elem2.Id);
                var mappings = elem2.Properties.ToList();
                Assert.Equal(["value"], mappings[0].Attributes);
                Assert.Equal("Property1", mappings[0].ToProperty);
            });
    }

    [Fact]
    public void Overide_APageObjectSchemaId_Matches_Merges_MappedOverlap_FavouringRequest()
    {
        // Arrange
        IEnumerable<PageObjectSchema> seed = [
            new PageObjectSchema
                    {
                        Id = "PAGEOBJECT_ID_1",
                        Properties = [
                            new PropertyMapping()
                            {
                                Attributes = ["text"],
                                ToProperty = "MyOutputProperty1"
                            }
                        ]
                    }
        ];

        IEnumerable<PageObjectSchema> overrideSchema = [
            new PageObjectSchema()
                {
                    Id = "PAGEOBJECT_ID_2",
                    Properties =
                    [
                        new PropertyMapping()
                        {
                            Attributes = ["value"],
                            ToProperty = "MyOutputProperty2"
                        }
                    ],
                    Children = []
                },
            new PageObjectSchema()
                {
                    Id = "PAGEOBJECT_ID_1",
                    Properties =
                    [
                        new PropertyMapping()
                        {
                            Attributes = ["ATTRIBUTE1", "ATTRIBUTE@2", "1ATTRIBUTE%3"],
                            ToProperty = "MyOutputProperty1"
                        },
                        new PropertyMapping()
                        {
                            Attributes = ["else", "a", "£$%£$^S"],
                            ToProperty = "NotOverwrittenProperty3"
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
                Assert.Equal("PAGEOBJECT_ID_1", elem1.Id);
                var resultMappings = elem1.Properties.ToList();
                Assert.Equal(["ATTRIBUTE1", "ATTRIBUTE@2", "1ATTRIBUTE%3"], resultMappings[0].Attributes);
                Assert.Equal("MyOutputProperty1", resultMappings[0].ToProperty);

                var firstMappedAttribute = elem1.Properties.ToList();
                Assert.Equal(["else", "a", "£$%£$^S"], resultMappings[1].Attributes);
                Assert.Equal("NotOverwrittenProperty3", resultMappings[1].ToProperty);
            },
            elem2 =>
            {
                Assert.Equal("PAGEOBJECT_ID_2", elem2.Id);
                var firstMappedAttribute = elem2.Properties.ToList();
                Assert.Equal(["value"], firstMappedAttribute[0].Attributes);
                Assert.Equal("MyOutputProperty2", firstMappedAttribute[0].ToProperty);
            });
    }
}
