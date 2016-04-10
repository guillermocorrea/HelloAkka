using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;
using MovieStreaming.Messages;

namespace MovieStreaming.Actors
{
    public class PlaybackActor : ReceiveActor
    {
        public PlaybackActor()
        {
            Console.WriteLine("Creating a PlaybackActor...");
            ReceiveAsync<PlayMovieMessage>(message => HandlePlayMovieMessageAsync(message));
        }

        private Task HandlePlayMovieMessageAsync(PlayMovieMessage message)
        {
            ColorConsole.WriteLineYellow(string.Format("Play movie '{0}' for user '{1}'",
                message.MovieTitle,
                message.UserId));
            return Task.FromResult<object>(null);
        }

        protected override void PreStart()
        {
            ColorConsole.WriteLineGreen("PlaybackActor PreStart");
            base.PreStart();
        }


        protected override void PostStop()
        {
            ColorConsole.WriteLineGreen("PlaybackActor PostStop");
            base.PostStop();
        }

        protected override void PreRestart(Exception reason, object message)
        {
            ColorConsole.WriteLineGreen("PlaybackActor PreRestart because: " + reason);
            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            ColorConsole.WriteLineGreen("PlaybackActor PostRestart because: " + reason);
            base.PostRestart(reason);
        }
    }
}
