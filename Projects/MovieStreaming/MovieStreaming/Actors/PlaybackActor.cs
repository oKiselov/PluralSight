using System;
using Akka.Actor;

namespace MovieStreaming.Actors
{
    public class PlaybackActor: UntypedActor
    {
        public PlaybackActor()
        {
            Console.WriteLine("Creating a PlaybackActor");
        }

        protected override void OnReceive(object message)
        {
            throw new System.NotImplementedException();
        }
    }
}