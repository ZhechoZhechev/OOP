namespace Robots.Tests
{
    using System;
    using NUnit.Framework;

    [TestFixture]
    public class RobotsTests
    {
        private const string ROBOT_NAME = "TestName";
        private const int ROBOT_MAX_BATTERY = 100;
        private const int MANAGER_CAPACITY = 10;

        private Robot robot;
        private RobotManager robotManager;

        [SetUp]
        public void SetUp()
        {
            robot = new Robot(ROBOT_NAME, ROBOT_MAX_BATTERY);
            robotManager = new RobotManager(MANAGER_CAPACITY);
        }

        [Test]
        public void Robot_ConstructorWorksAsIntended()
        {
            Assert.IsTrue(robot.Name == ROBOT_NAME
                && robot.MaximumBattery == ROBOT_MAX_BATTERY && robot.Battery == ROBOT_MAX_BATTERY);
        }

        [Test]
        public void RobotManager_ConstructorWorksAsIntended()
        {
            robotManager.Add(robot);
            Assert.IsTrue(robotManager.Capacity == MANAGER_CAPACITY
                && robotManager.Count == 1);
        }

        [Test]
        public void Capacity_EceptionIfBellowZero()
        {
            Assert.That(() =>
            {
                RobotManager manager = new RobotManager(-123);
            }, Throws.TypeOf<ArgumentException>()
            .With.Message.EqualTo("Invalid capacity!"));
        }

        [Test]
        public void Add_ExceptionIfRobotAlreadyExists()
        {
            robotManager.Add(robot);

            Assert.That(() =>
            {
                robotManager.Add(robot);

            }, Throws.TypeOf<InvalidOperationException>()
            .With.Message.EqualTo($"There is already a robot with name {robot.Name}!"));
        }

        [Test]
        public void Add_ExceptionIfCapacityIsFull()
        {
            RobotManager manager = new RobotManager(1);
            Robot robotche = new Robot("Teneke", 99);
            manager.Add(robot);

            Assert.That(() =>
            {
                manager.Add(robotche);

            }, Throws.TypeOf<InvalidOperationException>()
            .With.Message.EqualTo("Not enough capacity!"));
        }

        [Test]
        public void Add_IncreasesCountByOne()
        {
            robotManager.Add(robot);

            Assert.AreEqual(robotManager.Count, 1);
        }

        [Test]
        public void Remove_ExceptionWhenRobotDoesNotExist()
        {
            robotManager.Add(robot);

            Assert.That(() =>
            {
                robotManager.Remove("Bender");

            }, Throws.TypeOf<InvalidOperationException>()
            .With.Message.EqualTo("Robot with the name Bender doesn't exist!"));
        }

        [Test]
        public void Remove_DecresesCountWithOne()
        {
            robotManager.Add(robot);
            robotManager.Remove(robot.Name);

            Assert.AreEqual(robotManager.Count, 0);
        }

        [Test]
        public void Work_ExceptionWhenRobotNameDoesNotExist()
        {
            robotManager.Add(robot);

            Assert.That(() =>
            {
                robotManager.Work("Bender", "drink", 30);

            }, Throws.TypeOf<InvalidOperationException>()
            .With.Message.EqualTo("Robot with the name Bender doesn't exist!"));
        }

        [Test]
        public void Work_ExceptionIfNotEnoughBattery()
        {
            Robot roboto = new Robot("Bender", 33);
            robotManager.Add(roboto);

            Assert.That(() =>
            {
                robotManager.Work("Bender", "drink", 34);

            }, Throws.TypeOf<InvalidOperationException>()
            .With.Message.EqualTo($"{roboto.Name} doesn't have enough battery!"));
        }

        [Test]
        public void Work_WorksAsIntended()
        {
            Robot roboto = new Robot("Bender", 33);

            robotManager.Add(roboto);
            robotManager.Work("Bender", "drink", 32);

            Assert.AreEqual(roboto.Battery, 1);
        }

        [Test]
        public void Charge_ExceptionWhenRobotNameDoesNotExist()
        {
            robotManager.Add(robot);

            Assert.That(() =>
            {
                robotManager.Charge("Bender");

            }, Throws.TypeOf<InvalidOperationException>()
            .With.Message.EqualTo("Robot with the name Bender doesn't exist!"));
        }

        [Test]
        public void Charge_WorksAsIntended()
        {
            robotManager.Add(robot);
            robotManager.Work(ROBOT_NAME, "clean", 50);
            robotManager.Charge(ROBOT_NAME);

            Assert.AreEqual(robot.Battery, ROBOT_MAX_BATTERY);
        }
    }
}
