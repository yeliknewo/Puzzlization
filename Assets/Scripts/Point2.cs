using System;

public class Point2: IEquatable<Point2>
{
	private short _x = 0;
	public short x {
		private set {
			_x = value;
		}
		get {
			return _x;
		}
	}

	private short _b = 0;
	public short y {
		private set {
			_b = value;
		}
		get {
			return _b;
		}
	}

	public Point2(short newX = 0, short newY = 0) {
		this.x = newX;
		this.y = newY;
	}

	public bool Equals (Point2 other)
	{
		return other.x == this.x && other.y == this.y;
	}
		
	public override int GetHashCode ()
	{
		Byte[] bytes = new Byte[4];
		{
			Byte[] temp = BitConverter.GetBytes (x);
			bytes [0] = temp [0];
			bytes [1] = temp [1];
		}
		{
			Byte[] temp = BitConverter.GetBytes (y);
			bytes [2] = temp [0];
			bytes [3] = temp [1];
		}
		return BitConverter.ToInt32(bytes, 0);
	}
}
