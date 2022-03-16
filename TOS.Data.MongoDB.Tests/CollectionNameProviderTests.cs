using Moq;
using NUnit.Framework;
using TOS.Common.MongoDB;
using TOS.Common.Text.Semantics;

namespace TOS.Data.MongoDB.Tests
{
    public class CollectionNameProviderTests
    {
        private const string CollectionName = "TypeIes";

        private Mock<IPluralizer> _pluralizer;
        private CollectionNameProvider _collectionNameProvider;


        public class TypeA
        {

        }


        [CollectionName(CollectionName)]
        public class TypeY
        {

        }

        [SetUp]
        public void Setup()
        {
            _pluralizer = new Mock<IPluralizer>();
            _collectionNameProvider = new CollectionNameProvider(_pluralizer.Object);
        }

        [Test]
        public void GetCollectionName_WhenTypeDoesNotHaveCustomName_ShouldPluralize()
        {
            const string expectedName = "TypeAs";
            _pluralizer
                .Setup(p => p.Pluralize(typeof(TypeA).Name))
                .Returns(expectedName);
            string actualName = _collectionNameProvider.GetCollectionName<TypeA>();
            Assert.AreEqual(expectedName, actualName);
        }

        [Test]
        public void GetCollectionName_WhenTypeHasCustomName_ShouldReturnCustomName()
        {
            string actualName = _collectionNameProvider.GetCollectionName<TypeY>();
            Assert.AreEqual(CollectionName, actualName);
        }
    }
}