namespace CodeQuality.Samples.Legacy.Seams;

public class TripService
{
    private readonly TripRepository repository = new();

    public IEnumerable<Trip> GetTrips(User user)
    {
        var trips = new List<Trip>();
        var connectedUser = UserSession.GetConnectedUser();
        var isFriend = false;
        if (connectedUser != null)
        {
            foreach (var friend in user.GetFriends())
            {
                if (friend.Equals(connectedUser))
                {
                    isFriend = true;
                    break;
                }
            }

            if (isFriend)
            {
                trips = this.repository.FindTrips(user).ToList();
            }

            return trips;
        }

        throw new UserNotConnectedException();
    }
}

public class UserNotConnectedException : Exception
{
}

public record Trip(string Destination);

public class User
{
    private readonly List<User> friends = new();
    private readonly List<Trip> trips = new();

    public void AddFriend(User friend)
    {
        friends.Add(friend);
    }

    public void AddTrip(Trip trip)
    {
        trips.Add(trip);
    }

    public IEnumerable<User> GetFriends()
    {
        return new List<User>(friends);
    }

    public IEnumerable<Trip> GetTrips()
    {
        return new List<Trip>(trips);
    }
}

public static class UserSession
{
    public static User GetConnectedUser()
    {
        throw new Exception("Cannot use UserSession in tests.");
    }
}

public class TripRepository
{
    public IEnumerable<Trip> FindTrips(User user)
    {
        throw new Exception("Cannot use TripRepository in tests.");
    }
}