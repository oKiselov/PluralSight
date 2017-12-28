using System.Collections.Generic;
using Akka.Actor;
using MovieStreaming.Messages;

namespace MovieStreaming.Actors
{
    public class UserCoordinatorActor: ReceiveActor
    {
        private readonly Dictionary<int, IActorRef> _users;

        private UserCoordinatorActor()
        {
            _users = new Dictionary<int, IActorRef>();
            Receive<PlayMovieMessage>(
                message =>
                {
                    CreateChildUserIfNotExists(message.UserId);
                });
        }

        protected override void PreStart()
        {
            ColorConsole.WriteLineCyan("UserCoordinatorActor PreStart");
        }

        protected override void PostStop()
        {
            ColorConsole.WriteLineCyan("UserCoordinatorActor PostStop");
        }
    }
}