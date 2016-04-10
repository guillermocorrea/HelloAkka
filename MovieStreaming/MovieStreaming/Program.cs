using Akka.Actor;
using MovieStreaming.Actors;
using MovieStreaming.Messages;
using System;
using System.Threading.Tasks;

namespace MovieStreaming
{
    class Program
    {
        private static ActorSystem MovieStreamingActorSystem;

        static void Main(string[] args)
        {
            MovieStreamingActorSystem = ActorSystem.Create("MovieStreamingActorSystem");
            Console.WriteLine("Actor system created");

            Props userActorProps = Props.Create<UserActor>();
            IActorRef userActorRef = MovieStreamingActorSystem.ActorOf(userActorProps, "UserActor");

            Console.ReadKey();
            Console.WriteLine("Sending a PlayMovieMessage (Codenan the destroyer)");
            userActorRef.Tell(new PlayMovieMessage("Codenan the destroyer", 42));

            Console.ReadKey();
            Console.WriteLine("Sending a PlayMovieMessage (Boolean lies)");
            userActorRef.Tell(new PlayMovieMessage("Boolean lies", 42));

            Console.ReadKey();
            Console.WriteLine("Sending a StopMovieMessage");
            userActorRef.Tell(new StopMovieMessage());

            Console.ReadKey();
            Console.WriteLine("Sending another StopMovieMessage");
            userActorRef.Tell(new StopMovieMessage());

            /*
            Props playbackActorProps = Props.Create<PlaybackActor>();
            IActorRef playbackActorRef = MovieStreamingActorSystem.ActorOf(playbackActorProps, "PlaybackActor");
            
            playbackActorRef.Tell(new PlayMovieMessage("Akka the movie", 42));
            playbackActorRef.Tell(new PlayMovieMessage("Fast N Furious", 43));
            playbackActorRef.Tell(new PlayMovieMessage("The Revenant", 44));
            playbackActorRef.Tell(new PlayMovieMessage("007", 45));

            playbackActorRef.Tell(PoisonPill.Instance);
            */

            Console.ReadLine();

            MovieStreamingActorSystem.Terminate();

            Task task = MovieStreamingActorSystem.WhenTerminated;
            task.Wait();
            Console.WriteLine("Actor system terminated");

            Console.ReadLine();
        }
    }
}
