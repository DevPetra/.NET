using System.Collections;

namespace Work_01
{
	public class Node<T>
	{
		public T Value { get; set; }
		public Node<T> Next { get; set; }

		public Node(T value)
		{
			Value = value;
		}
	}
	public class MyLinkedList<T> : IEnumerable<T>
	{
		public Node<T> Head { get; private set; }
		public Node<T> Tail { get; private set; }
		public int Count { get; private set; }
		public object SyncRoot { get; private set; } = new object();

		public MyLinkedList() { }

		public bool IsSynchronized() => false;

		public void Add(T value)      //performance is O(1) because I keep track of the tail, so it always takes the same time
		{
			Node<T> newNode = new Node<T>(value);

			if (Head == null)
			{
				Head = newNode;
				Tail = newNode;
			}
			else
			{
				Tail.Next = newNode;
				Tail = newNode;
			}
			Count++;
		}

		public void Remove(T value)   // performace is O(n) because I need to search the list to find the required value
		{
			var current = Head;
			Node<T> previous = null;

			if (!Contains(value))
			{
				return;
			}

			while (current != null)
			{
				if (current.Value.Equals(value))
				{
					if (previous != null)
					{
						previous.Next = current.Next;

						if (current.Next == null)
						{
							Tail = previous;
						}
					}
					else
					{
						Head = Head.Next;

						if (Head == null)
						{
							Tail = null;
						}
					}
					Count--;
					return;
				}
				previous = current;
				current = current.Next;
			}
		}

		public void Clear()
		{
			var current = Head;
			while (current != null)
			{
				current = null;
				current.Next = current;
			}

			Count = 0;
		}

		public bool Contains(T value)     // performance is O(n), because I need to search the whole linked list
		{
			var current = Head;
			while (current != null)
			{
				if (current.Value.Equals(value))
				{
					return true;
				}
				current = current.Next;

			}
			return false;
		}

		public void CopyTo(T[] array, int arrIndex)
		{
			var current = Head;
			while (current != null)
			{
				array[arrIndex++] = current.Value;
				current = current.Next;
			}
		}

		public IEnumerator<T> GetEnumerator()       //for generic collections
		{
			var current = Head;
			while (current != null)
			{
				yield return current.Value;
				current = current.Next;
			}
		}

		IEnumerator IEnumerable.GetEnumerator()     //non generic collections
		{
			return GetEnumerator();
		}

	}

}
