namespace Coding.DataStructure.PopularityTrack;

public interface IPopularityTrackService
{
    void IncreasePopularity(int contentId);
    void DecreasePopularity(int contentId);
    int GetMostPopularContent();
}