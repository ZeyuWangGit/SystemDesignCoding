namespace Coding.Karat;

public class FriendsRelationshipTests
{
    [Fact]
    public void Test_BuildFriendshipMap()
    {
        var friendships = new List<(int, int)>
        {
            (1, 2), (1, 3), (2, 4), (3, 4), (5, 6)
        };

        var expected = new Dictionary<int, HashSet<int>>
        {
            {1, [2, 3] },
            {2, [1, 4] },
            {3, [1, 4] },
            {4, [2, 3] },
            {5, [6] },
            {6, [5] }
        };

        var result = FriendsRelationshipCode.BuildFriendshipMap(friendships);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Test_CountCrossDepartmentFriends()
    {
        var friendships = new List<(int, int)>
        {
            (1, 2), (1, 3), (2, 4), (3, 4), (5, 6)
        };

        var departments = new Dictionary<int, string>
        {
            {1, "HR"},
            {2, "HR"},
            {3, "Finance"},
            {4, "Finance"},
            {5, "IT"},
            {6, "IT"}
        };

        var expected = new Dictionary<string, int>
        {
            {"HR", 2},
            {"Finance", 2},
            {"IT", 0}
        };

        var result = FriendsRelationshipCode.CountCrossDepartmentFriends(friendships, departments);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Test_IsSingleSocialCircle()
    {
        var friendships1 = new List<(int, int)>
        {
            (1, 2), (2, 3), (3, 4), (5, 6)
        };

        var friendships2 = new List<(int, int)>
        {
            (1, 2), (2, 3), (3, 4)
        };

        Assert.False(FriendsRelationshipCode.IsSingleSocialCircle(friendships1)); // 两个独立社交圈
        Assert.True(FriendsRelationshipCode.IsSingleSocialCircle(friendships2));  // 只有一个社交圈
    }
    
    
}