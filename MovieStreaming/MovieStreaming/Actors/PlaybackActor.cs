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
            ReceiveAsync<PlayMovieMessage>(message => HandlePlayMovieMessageAsync(message), message => message.UserId == 42);
        }

        private Task HandlePlayMovieMessageAsync(PlayMovieMessage message)
        {
            Console.WriteLine("Received: " + message.MovieTitle);
            Console.WriteLine("Received User ID: " + message.UserId);
            return Task.FromResult<object>(null);
        }
    }
}
