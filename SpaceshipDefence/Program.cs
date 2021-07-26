using System;
using System.Collections.Generic;
using System.Linq;

namespace SpaceshipDefence
{
    class Program
    {
        static void Main(string[] args)
        {
            //teleporters
            //turbo lifts

            //teleport to room same color
            //lifts from 1 room to other
            //one way

            //for each soldier minimum amout of time to travel from location to destination

            //N number of rooms
            //1 to N rooms
            //n lines with 

            //number of turbolifts
            //ai, bi, ti   //ift from a to b in t seconds

            //S number of soldiers
            //pj, qj //soldier location and destination

            var T = int.Parse(Console.ReadLine());
            for (int i = 0; i < T; i++)
            {
                Func(i);
            }
        }

        public class RoomColor
        {
            public List<int> RoomIds { get; set; } = new List<int>();
            public List<(int to, int time)> Lifts { get; set; } = new List<(int to, int time)>();
        }


        public static void Func(int testcaseNum)

        {
            Dictionary<string, RoomColor> Rooms = new Dictionary<string, RoomColor>();
            List<(int from, int to)> Soldiers = new List<(int from, int to)>();
            var roomsToColor = new Dictionary<int, string>();

            var roomCount = int.Parse(Console.ReadLine());
            for (int i = 0; i < roomCount; i++)
            {
                var color = Console.ReadLine();
                if (!Rooms.ContainsKey(color))
                {
                    Rooms[color] = new RoomColor();
                }
                Rooms[color].RoomIds.Add(i + 1);
                roomsToColor[i + 1] = color;
            }
            var liftCount = int.Parse(Console.ReadLine());
            for (int i = 0; i < liftCount; i++)
            {
                var r = Console.ReadLine().Split(" ");
                var from = int.Parse(r[0]);
                var to = int.Parse(r[1]);
                var time = int.Parse(r[2]);
                var roomColor = Rooms.FirstOrDefault(x => x.Value.RoomIds.Any(y => y == from));
                roomColor.Value.Lifts.Add((to, time));
                
            }
            var soldierCount = int.Parse(Console.ReadLine());
            for (int i = 0; i < soldierCount; i++)
            {
                var r = Console.ReadLine().Split(" ");
                Soldiers.Add((int.Parse(r[0]), int.Parse(r[1])));
            }

            //output seconds to get to dest or -1 if impossible
            //80 000 rooms
            //3 000 soldiers

            Console.WriteLine("Case #" + (testcaseNum + 1) + ":");
            

            foreach (var s in Soldiers)
            {
                
                var t = SoldierFindDest(s.from, s.to, Rooms, roomsToColor);
                Console.WriteLine(t);
                //Search through all lifts
            }
            Console.Write(Environment.NewLine);

        }

        public static int SoldierFindDest(int from, int to, Dictionary<string, RoomColor> rooms, Dictionary<int, string> roomsToColor)
        {
            var Paths = new List<(int from, int time)>();
            var visitedRoomColors = new List<string>();
            Paths.Add((from, 0));
            while (true)
            {
                var fp = Paths.OrderBy(x => x.time).FirstOrDefault();
                if (Paths.Count == 0)
                {                    
                    return - 1;
                }
                visitedRoomColors.Add(roomsToColor[fp.from]);

                Paths.Remove(fp);
                var f = fp.from;
                var ti = fp.time;
                
                
                var currentRoom = rooms[roomsToColor[f]];
                if (currentRoom.RoomIds.Any(x => x == to))
                {
                    //goal reached
                    return ti;
                }

                foreach (var l in currentRoom.Lifts)
                {
                    var toColor = roomsToColor[l.to];
                    if (!visitedRoomColors.Contains(toColor))
                    {
                        Paths.Add((l.to, ti + l.time));
                    }
                }
            }            
        }
    }
}
