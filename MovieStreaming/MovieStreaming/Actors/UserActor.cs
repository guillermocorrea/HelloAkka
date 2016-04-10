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

            ColorConsole.WriteLineCyan("Setting initial behaviour to stopped");
            Stopped();
            /*ReceiveAsync<PlayMovieMessage>(message => HandlePlayMovieMessageAsync(message));
            ReceiveAsync<StopMovieMessage>(message => HandleStopMovieMessageAsync(message));*/
        }

        private void Stopped()
        {
            ReceiveAsync<PlayMovieMessage>(message => StartMoviePlaying(message.MovieTitle));
            ReceiveAsync<StopMovieMessage>(message => {
                ColorConsole.WriteLineRed("Error: cannot stop movie if nothing is playing");
                return Task.FromResult<object>(null);
            });

            ColorConsole.WriteLineCyan("UserActor state has become Stopped");
        }

        private void Playing()
        {
            ReceiveAsync<PlayMovieMessage>(message => {
                ColorConsole.WriteLineRed("Error: cannot start playing another movie before stopping existing one");
                return Task.FromResult<object>(null);
            });
            ReceiveAsync<StopMovieMessage>(message => StopPlayingCurrentMovie());

            ColorConsole.WriteLineCyan("UserActor state has become Playing");
        }

        /*private Task HandleStopMovieMessageAsync(StopMovieMessage message)
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
        }*/

        private Task StopPlayingCurrentMovie()
        {
            ColorConsole.WriteLineYellow(string.Format("User has stopped watching movie '{0}' ", _currentlyWatching));
            _currentlyWatching = null;
            Become(Stopped);

            return Task.FromResult<object>(null);
        }

        /*private Task HandlePlayMovieMessageAsync(PlayMovieMessage message)
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
        }*/

        private Task StartMoviePlaying(string movieTitle)
        {
            _currentlyWatching = movieTitle;
            ColorConsole.WriteLineYellow(string.Format("Currently watching '{0}'", _currentlyWatching));

            Become(Playing);
            return Task.FromResult<object>(null);
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
