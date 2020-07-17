using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class Level
{
    public Level(string path, int lives, string songFile, string background, string title, string artist, string creator, string icon,
        List<EnemyEvent> enemies, List<SpeedEvent> speeds)
    {
        Path = path;
        Lives = lives;
        SongFile = songFile;
        Background = background;
        Title = title;
        Artist = artist;
        Creator = creator;
        Icon = icon;
        Enemies = enemies;

        if (speeds[0].Speed != 0) speeds.Insert(0, new SpeedEvent(100, 0));

        Speeds = speeds;
    }

    public Level() { }

    [Key] public int Id { get; set; }
    public string Path { get; set; }
    public int Lives { get; set; }
    public string SongFile { get; set; }
    public string Background { get; set; }
    public string Title { get; set; }
    public string Artist { get; set; }
    public string Creator { get; set; }
    public string Icon { get; set; }
    public List<EnemyEvent> Enemies { get; set; } = new List<EnemyEvent>();
    public List<SpeedEvent> Speeds { get; set; } = new List<SpeedEvent>();

    public override string ToString() =>
        $"{nameof(Path)}: {Path}, {nameof(Lives)}: {Lives}, {nameof(SongFile)}: {SongFile}, {nameof(Background)}: {Background}, {nameof(Title)}: {Title}, {nameof(Artist)}: {Artist}, {nameof(Creator)}: {Creator}, {nameof(Icon)}: {Icon}, {nameof(Enemies)}: {Enemies}, {nameof(Speeds)}: {Speeds}";
}

public class EnemyEvent
{
    public EnemyEvent(int killKey, float rotation, float rotationSpeed, float spawnTime)
    {
        this.KillKey = killKey;
        this.Rotation = rotation;
        this.RotationSpeed = rotationSpeed;
        this.SpawnTime = spawnTime;
    }

    public EnemyEvent() { }

    public int KillKey { get; set; }
    public float Rotation { get; set; }
    public float RotationSpeed { get; set; }
    public float SpawnTime { get; set; }

    public override string ToString() => $"{nameof(KillKey)}: {KillKey}, {nameof(this.Rotation)}: {this.Rotation}, {nameof(RotationSpeed)}: {RotationSpeed}, {nameof(SpawnTime)}: {SpawnTime}";
}

public class SpeedEvent
{
    public SpeedEvent(float speed, float spawnTime)
    {
        Speed = speed;
        SpawnTime = spawnTime;
    }

    public SpeedEvent() { }

    public float Speed { get; set; }
    public float SpawnTime { get; set; }

    public override string ToString() => $"{nameof(Speed)}: {Speed}, {nameof(SpawnTime)}: {SpawnTime}";
}