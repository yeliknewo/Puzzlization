using System;

public class Point2<T>: IEquatable<Point2<T>>
	where T: IEquatable<T>
{
	private T[] coords = new T[2];

	public T a {
		private set {
			coords[0] = value;
		}
		get {
			return coords[0];
		}
	}

	public T b {
		private set {
			coords[1] = value;
		}
		get {
			return coords[1];
		}
	}

	public Point2(T newA = default(T), T newB = default(T)) {
		this.a = newA;
		this.b = newB;
	}

	public bool Equals (Point2<T> other)
	{
		return other.a.Equals(this.a) && other.b.Equals(this.b);
	}

	public override string ToString ()
	{
		return string.Format ("[Point2: a={0}, b={1}]", a, b);
	}
}
