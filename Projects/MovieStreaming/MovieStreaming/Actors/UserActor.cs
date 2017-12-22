using System;
using System.Threading.Tasks;
using Akka.Actor;
using MovieStreaming.Messages;

namespace MovieStreaming.Actors
{
    public class UserActor : ReceiveActor
    {
        private string _currentlyWatching;

        public UserActor()
        {
            Console.WriteLine("Creating a UserActor");
            ColorConsole.WriteLineCyan("Setting initial behaviour to stopped");
            Stopped();
        }

        private void Playing()
        {
            Receive<PlayMovieMessage>(
                message => ColorConsole.WriteLineRed("Error: cannot start new movie before stoping existing one"));
            Receive<StopMovieMessage>(message => StopPlayingCurrentMovie());
            ColorConsole.WriteLineCyan("UserActor has now become playing");
        }

        private void Stopped()
        {
            Receive<PlayMovieMessage>(message => StartPlayingMovie(message.MovieTitle));
            Receive<StopMovieMessage>(
                message => ColorConsole.WriteLineRed("Error: cannot stop if nothing is playing"));
            ColorConsole.WriteLineCyan("UserActor has now become stopped");
        }

        private void StopPlayingCurrentMovie()
        {
            ColorConsole.WriteLineYellow(String.Format("User has stopped watching " + _currentlyWatching));
            _currentlyWatching = null;
            Become(Stopped);
        }

        private void StartPlayingMovie(string title)
        {
            _currentlyWatching = title;

            ColorConsole.WriteLineYellow(String.Format("User is currently watching " + _currentlyWatching));

            Become(Playing);
        }

        protected override void PreStart()
        {
            ColorConsole.WriteLineGreen("UserActor PreStart");
        }

        protected override void PostStop()
        {
            ColorConsole.WriteLineGreen("UserActor PostStop");
        }

        protected override void PreRestart(Exception reason, object message)
        {
            ColorConsole.WriteLineGreen("UserActor PreRestart, reason: " + reason);
            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            ColorConsole.WriteLineGreen("UserActor PostRestart, reason: " + reason);
            base.PostRestart(reason);
        }
    }
}