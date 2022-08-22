namespace Api.Players;

public interface ISessionService
{
    IList<Player> Sessions {get;}
    bool DeleteSession(string username);
    bool CreateSession(string username);
    void IncreaseScore(string username);
}

public class SessionService : ISessionService
{
    private readonly IList<Player> _sessions = new List<Player>();
    public IList<Player> Sessions => _sessions;
    
    public bool DeleteSession(string username) {
        var session = Sessions.FirstOrDefault(x => x.Name == username);

        if (session == null) {
            return false;
        }

        Sessions.Remove(session);

        return true;
    }

    public bool CreateSession(string username)
    {
        var exists = Sessions.Any(x => x.Name == username);

        if (exists) {
            return false;
        }

        Sessions.Add(new Player(username, 0));

        return true;
    }

    public void IncreaseScore(string username)
    {
        Sessions.First(x => x.Name == username).Score += 1;
    }
}