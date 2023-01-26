using NUnit.Framework;

namespace Collections.Tests
{
    public class CollectionTests
    {
        [Test]
       public void Test_Collection_EmptyConstructor()
        {
            //Arange
            var nums = new Collection<int>();

            //Assert
            Assert.That(nums.ToString(), Is.EqualTo("[]"));
        }

        [Test]
        public void Test_Collection_SingleItem()
        {
            var nums = new Collection<int>(5);

            Assert.That(nums[0], Is.EqualTo(5));
        }

        [Test]
        public void Test_Collection_MultipleItems()
        {
            var nums = new Collection<int>(1, 2, 3);

            Assert.That(nums.ToString(), Is.EqualTo("[1, 2, 3]"));
        }

        [Test]
        public void Test_Collection_Add()
        {
            var nums = new Collection<int>();
            nums.Add(10);
            nums.Add(20);

            Assert.That(nums.ToString(), Is.EqualTo("[10, 20]"));
        }

        [Test]
        public void Test_Collection_AddRangeWithGrow()
        {
            var nums = new Collection<int>();

            int oldCapacity = nums.Capacity;
            var newNums = Enumerable.Range(1000, 2000).ToArray();

            nums.AddRange(newNums);

            string expectedNums = "[" + String.Join(", ", newNums) + "]";

            Assert.That(nums.ToString(), Is.EqualTo(expectedNums));
            Assert.That(nums.Capacity, Is.GreaterThanOrEqualTo(oldCapacity));
            Assert.That(nums.Capacity, Is.GreaterThanOrEqualTo(nums.Count));
        }

        [Test]
        public void Test_Collection_GetByIndex()
        {
            var names = new Collection<string>("Peter", "Maria");

            var firstItem = names[0];
            var secondItem = names[1];

            Assert.That(firstItem, Is.EqualTo("Peter"));
            Assert.That(secondItem, Is.EqualTo("Maria"));
        }

        [Test]
        public void Test_Collection_GetByInvalidIndex()
        {
            var names = new Collection<string>("Peter", "Maria");

            Assert.That(() => { var name = names[-1]; }, Throws.InstanceOf<ArgumentOutOfRangeException>());
            Assert.That(() => { var name = names[2]; }, Throws.InstanceOf<ArgumentOutOfRangeException>());
            Assert.That(() => { var name = names[500]; }, Throws.InstanceOf<ArgumentOutOfRangeException>());
            Assert.That(names.ToString(), Is.EqualTo("[Peter, Maria]"));
        }

        [Test]
        public void Test_Collection_SetByIndex()
        {
            var names = new Collection<string>("Peter", "Maria");

            names[1] = "Atanaska";

            Assert.That(names.ToString(), Is.EqualTo("[Peter, Atanaska]"));
        }

        [Test]
        public void Test_Collection_SetByInvalidIndex()
        {
            var names = new Collection<string>("Peter", "Maria");

            names[0] = "Gosho";

            Assert.That(() => { names[-1] = "Ivan"; }, Throws.InstanceOf<ArgumentOutOfRangeException>());
            Assert.That(() => { names[2] = "Ivan"; }, Throws.InstanceOf<ArgumentOutOfRangeException>());
            Assert.That(() => { names[500] = "Ivan"; }, Throws.InstanceOf<ArgumentOutOfRangeException>());
            Assert.That(names.ToString(), Is.EqualTo("[Gosho, Maria]"));
        }

        [Test]
        public void Test_Collection_InsertAtStart() 
        {
            var names = new Collection<string>("Gosho", "Maria");

            names.InsertAt(0, "Peter");

            Assert.That(names.ToString(), Is.EqualTo("[Peter, Gosho, Maria]"));
        }

        [Test]
        [Timeout(1000)] 
        public void Test_Collection_OneMilionItems()
        {
            const int itemsCount = 1000000;
            var nums = new Collection<int>();
            nums.AddRange(Enumerable.Range(1, itemsCount).ToArray());

            Assert.That(nums.Count == itemsCount);
            Assert.That(nums.Capacity >= nums.Count);
            for (int i = itemsCount - 1; i >= 0; i--)
            {
                nums.RemoveAt(i);
            }

            Assert.That(nums.ToString() == "[]");
            Assert.That(nums.Capacity >= nums.Count);
        }
    }
}