using Akka.Actor;
using MovieStreaming.Actors;
using MovieStreaming.Messages;
using System;

namespace MovieStreaming
{
    class Program
    {
        private static ActorSystem MovieStreamingActorSystem;

        static void Main(string[] args)
        {
            MovieStreamingActorSystem = ActorSystem.Create("MovieStreamingActorSystem");
            Console.WriteLine("Actor system created");

            Props playbackActorProps = Props.Create<PlaybackActor>();
            IActorRef playbackActorRef = MovieStreamingActorSystem.ActorOf(playbackActorProps, "PlaybackActor");

            playbackActorRef.Tell(new PlayMovieMessage("Akka the movie", 42));
            playbackActorRef.Tell(new PlayMovieMessage("Akka the movie", 43));

            Console.ReadLine();

            MovieStreamingActorSystem.Terminate();
        }
    }
}
