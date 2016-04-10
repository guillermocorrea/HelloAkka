using System.Threading.Tasks;
using Akka.Actor;
using System;
using MovieStreaming.Messages;

namespace MovieStreaming.Actors
{
    public class UserActor : ReceiveActor
    {
        private string _currentlyWatching;

        public UserActor()
        {
            Console.WriteLine("Creating a UserActor");

            ReceiveAsync<PlayMovieMessage>(message => HandlePlayMovieMessageAsync(message));
            ReceiveAsync<StopMovieMessage>(message => HandleStopMovieMessageAsync(message));
        }

        private Task HandleStopMovieMessageAsync(StopMovieMessage message)
        {
            if (_currentlyWatching == null)
            {
                ColorConsole.WriteLineRed("Error: cannot stop movie if nothing is playing");
            }
            else
            {
                StopPlayingCurrentMovie();
            }

            return Task.FromResult<object>(null);
        }

        private void StopPlayingCurrentMovie()
        {
            ColorConsole.WriteLineYellow(string.Format("User has stopped watching movie '{0}' ", _currentlyWatching));
            _currentlyWatching = null;
        }

        private Task HandlePlayMovieMessageAsync(PlayMovieMessage message)
        {
            if (_currentlyWatching != null)
            {
                ColorConsole.WriteLineRed("Error: cannot start playing another movie before stopping existing one");
            }
            else
            {
                StartMoviePlaying(message.MovieTitle);
            }

            return Task.FromResult<object>(null);
        }

        private void StartMoviePlaying(string movieTitle)
        {
            _currentlyWatching = movieTitle;
            ColorConsole.WriteLineYellow(string.Format("Currently watching '{0}'", _currentlyWatching));
        }

        protected override void PreStart()
        {
            ColorConsole.WriteLineGreen("UserActor PreStart");
            base.PreStart();
        }


        protected override void PostStop()
        {
            ColorConsole.WriteLineGreen("UserActor PostStop");
            base.PostStop();
        }

        protected override void PreRestart(Exception reason, object message)
        {
            ColorConsole.WriteLineGreen("UserActor PreRestart because: " + reason);
            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            ColorConsole.WriteLineGreen("UserActor PostRestart because: " + reason);
            base.PostRestart(reason);
        }
    }
}
