using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Battleships;
using FluentAssertions;
using Xunit;

namespace Battleships.Test
{
    public class GameTest
    {
        [Fact]
        public void TestPlay()
        {
            var ships = new[] { "3:2,3:5" };
            var guesses = new[] { "7:0", "3:3" };
            Game.Play(ships, guesses).Should().Be(0);
        }
        [Fact]
        public void TestPlayShipSunk()
        {
            var ships = new[] { "3:2,3:5" };
            var guesses = new[] { "7:0", "3:3", "3:2" ,"3:4","3:5"};
            Game.Play(ships, guesses).Should().Be(1);
        }

        [Fact   
        public void TestPlayOneShipSunk()
        {
            var ships = new[] { "3:2,3:5","4:3,7:3" };
            var guesses = new[] { "7:0", "3:3", "3:2", "3:4", "3:5" };
            Game.Play(ships, guesses).Should().Be(1);
        }

    }
}
