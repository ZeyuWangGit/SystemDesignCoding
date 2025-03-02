namespace Coding.DSA.PopularityTrack;

public interface IPopularityTracker
{
    void IncreasePopularity(int contentId);
    void DecreasePopularity(int contentId);
    int GetMostPopularContent();
}