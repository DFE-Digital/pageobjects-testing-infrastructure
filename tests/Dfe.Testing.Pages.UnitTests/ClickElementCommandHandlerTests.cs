namespace Dfe.Testing.Pages.IntegrationTests;
public sealed class ClickElementCommandHandlerTests
{

    //TODO commented for now until we can remove the need for IDocumentQueryClientAccessor

    /*    [Fact]
        public void ShouldThrowIfConstructedWithNullAccessor()
        {
            Assert.Throws<ArgumentNullException>(() => new ClickElementCommandHandler(null!));
        }

        [Fact]
        public void ShouldThrowIfHandle_With_Null()
        {
            ClickElementCommandHandler handler = new(Substitute.For<IDocumentQueryClientAccessor>()!);
            Assert.Throws<ArgumentNullException>(() => handler.Handle(null!));
        }

        [Fact]
        public void ShouldThrowIfHandle_With_Null_FindBy()
        {
            ClickElementCommandHandler handler = new(Substitute.For<IDocumentQueryClientAccessor>()!);
            Assert.Throws<ArgumentNullException>(() => handler.Handle(new() { FindWith = null! }));
        }*/
}
