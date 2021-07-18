using Moq;
using MoviesAPI.Services;
using Xunit;

namespace MoviesAPI.UnitTests.Services
{
    public class MediatorServiceTests
    {
        private readonly Mock<IRatingHelper> _ratingHelper;
        private readonly MediatorService mediatorService;
        public MediatorServiceTests()
        {
            _ratingHelper = new Mock<IRatingHelper>();
            _ratingHelper.Setup(r => r.UpdateAverageRating(It.IsAny<int>()));
            mediatorService = new MediatorService(_ratingHelper.Object);
        }

        #region NotifyTests
        [Fact]
        public void Notify_should_notify_ratingHelper_to_update_average_ratings()
        {
            mediatorService.Nofity(1);
            _ratingHelper.Verify(r => r.UpdateAverageRating(It.Is<int>(i => i == 1)), Times.Once);
        }
        #endregion
    }
}
