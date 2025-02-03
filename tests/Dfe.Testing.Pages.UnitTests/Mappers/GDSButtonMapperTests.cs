using Dfe.Testing.Pages.Public;
using Dfe.Testing.Pages.Public.Components.MappingAbstraction.Request;
using Dfe.Testing.Pages.Public.Components.MappingAbstraction.Response;
using Dfe.Testing.Pages.Public.Components.Text;
using FluentAssertions;
using NSubstitute;

namespace Dfe.Testing.Pages.UnitTests.Mappers;
public sealed class GDSButtonMapperTests
{
    [Fact(Skip = "TODO UNIT TESTS ON MAPPERS")]
    public void Map()
    {
        IMapRequest<IDocumentSection> mapRequest = Substitute.For<IMapRequest<IDocumentSection>>();
        IDocumentSection section = Substitute.For<IDocumentSection>();
        mapRequest.Document.Returns(section);
        section.Text.Returns("blah");
        section.Document.Returns(string.Empty);

        TextMapper textMapper = new(
            Substitute.For<IMappingResultFactory>());

        MappedResponse<TextComponent> response = textMapper.Map(mapRequest);
        response.Mapped.Text.Should().Be("blah");
    }
}
