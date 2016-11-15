using System;

public class Point3<T>: IEquatable<Point3<T>>
	where T: IEquatable<T>
{
	private T[] coords = new T[3];

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

	public T c {
		private set {
			coords[2] = value;
		}
		get {
			return coords [2];
		}
	}

	public Point3(T newA = default(T), T newB = default(T), T newC = default(T)) {
		this.a = newA;
		this.b = newB;
		this.c = newC;
	}

	public bool Equals (Point3<T> other)
	{
		return this.a.Equals (other.a) && this.b.Equals (other.b) && this.c.Equals (other.c);
	}

	public override string ToString ()
	{
		return string.Format ("[Point3: a={0}, b={1}, c={2}]", a, b, c);
	}
}
