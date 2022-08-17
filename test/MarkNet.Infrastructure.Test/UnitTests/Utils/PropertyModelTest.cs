using MarkNet.Test.Models;

namespace MarkNet.Test.UnitTests.Utils
{
    public class PropertyModelTest
    {
        [Fact]
        public void ClearValue_InputModel_ReturnNullPropertyModel()
        {
            var model = new MockPropertyModel()
            {
                NormalInt = 1,
                NullableInt = 1,
                NormalString = "Normal",
                NullableString = "Normal",
                NormalTime = DateTime.Now,
                NullableTime = DateTime.Now,
            };

            model.ClearValues();

            Assert.Equal(0, model.NormalInt);
            Assert.Null(model.NullableInt);
            Assert.Null(model.NormalString);
            Assert.Null(model.NullableString);
            Assert.Equal(new DateTime(), model.NormalTime);
            Assert.Null(model.NullableTime);
        }
    }
}
