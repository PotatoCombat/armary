using System;

public class ActorEvent : GameEvent<Actor, ActorCommand> { }

[Serializable]
public class ActorEventListener : GameEventListener<ActorEvent, Actor, ActorCommand> { }
