using System;
using System.Collections;
using System.Collections.Generic;

// General object that has .CompareTo method 
//
namespace sorting {

	public class SortingBusiness<T> where T : IComparable
	{

		private List<T> array;
		private string sortType;
		private int length;

        public SortingBusiness()
		{
			array = new List<T>();
			sortType = "bubble";
			length = 0;
		}


        //constructor
        public SortingBusiness(List<T> array, string sortType)
		{
			this.array = array;
			this.sortType = sortType;
			this.length = array.Count;
		}

		private void GnomeSort()//Use at your own risk
		{
			int i = 0;
			while (i < length)
			{
				if (i == 0)
					i++;
				if (array[i].CompareTo(array[i - 1]) >= 0)
					i++;
				else
				{
					T temp = array[i];
					array[i] = array[i - 1];
					array[i - 1] = temp;
					i--;
				}
			}
		}

		private void BubbleSort()
		{
			int i, j;//counters
			T temp;//temporary variable
			bool swapped = false;       //flag to check if the array is sorted

			for (i = 0; i < length; i++)
			{
				swapped = false;//initialize flag
				for (j = 0; j < length - i - 1; j++)
					if (array[j].CompareTo(array[j + 1]) > 0)//if the first element is greater than the second
					{
						temp = array[j];
						array[j] = array[j + 1];
						array[j + 1] = temp;
						swapped = true;//swap and set flag to true
					}
                if (swapped == false)//if the array is sorted, break
                    break;
            }
			
		}
		void heapify(List<T> genericArray, int N, int i)//the heapify function is used to maintain the heap property when moving elements around
		{
			int largest = i; // initialize largest as root
			int l = 2 * i + 1; // left = 2*i + 1
			int r = 2 * i + 2; // right = 2*i + 2

			// If left child is larger than root
			if (l < N && genericArray[l].CompareTo(genericArray[largest]) == 1)
				largest = l;

			// If right child is larger than largest so far
			if (r < N && genericArray[r].CompareTo(genericArray[largest]) == 1)
				largest = r;

			// If largest is not root
			if (largest != i)
			{
				T swap = genericArray[i];
				genericArray[i] = genericArray[largest];
				genericArray[largest] = swap;

				// Recursively heapify the affected sub-tree
				heapify(genericArray , N, largest);
			}
		}

		private void HeapSort()
		{
			int lengthCopy = length;
			for (int i = lengthCopy / 2 - 1; i >= 0; i--)
				heapify(array, lengthCopy, i);
			for (int i = lengthCopy - 1; i > 0; i--)
			{
				T temporal = array[0];
				array[0] = array[i];
				array[i] = temporal;
				heapify(array, i, 0);
			}
		}


		private void MergeSort()
		{
			if (length < 2)
				return;

			int mid = length / 2;
			List<T> left = new List<T>(array.GetRange(0, mid)); // shallow copy of array on left, than the same on the right one
			List<T> right = new List<T>(array.GetRange(mid, length - mid));

			SortingBusiness<T> leftSorter = new SortingBusiness<T>(left, sortType); //initialise new objects to sort them wiht the merge 
			SortingBusiness<T> rightSorter = new SortingBusiness<T>(right, sortType);

			leftSorter.MergeSort();
			rightSorter.MergeSort();

			Merge(left, right);
		}

		private void Merge(List<T> left, List<T> right)
		{
			int leftLength = left.Count;
			int rightLength = right.Count;
			int i = 0, j = 0, k = 0;

			while (i < leftLength && j < rightLength)
			{
				if (left[i].CompareTo(right[j]) < 0) //if smaller or equal
				{
					array[k] = left[i];
					i++;
				}
				else //	greater
				{
					array[k] = right[j];
					j++;
				}
				k++;
			}

			while (i < leftLength) // copy the rest
			{
				array[k] = left[i];
				i++;
				k++;
			}

			while (j < rightLength) // copy the rest
			{
				array[k] = right[j];
				j++;
				k++;
			}
		}
		public List<T> Sort()
		{
			Console.WriteLine("Cases: Gnome , Bubble , Heap");
			switch (sortType)
			{
				case "Gnome":
					GnomeSort();
					return array;
				case "Bubble":
					BubbleSort();
					return array;
				case "Heap":
					HeapSort();
					return array;
				case "Merge":
					MergeSort();
					return array;
				default:
					Console.WriteLine("Invalid sort type");
					return array;
			}
		}
	}

}