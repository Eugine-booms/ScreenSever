using System.Windows.Markup;

namespace HappyNewYearScreenSaver;

[MarkupExtensionReturnType(typeof (double))]
internal class RND : MarkupExtension
{
	private Random? _rnd;
	public double Min { get; set; }
	public double Max { get; set; }

	private int _seed;
	public int Seed
	{
		get => _seed; 
		set
		{
			_seed = value;
			_rnd = new(value);
		}
	}
	public RND() { }
	public RND(double max) => this.Max = max;
	public RND(double max, double min) : this(max) => this.Min=min;
	public RND(double max, double min, int seed) : this(max, min) => this._seed=seed;

	public override object ProvideValue(IServiceProvider serviceProvider) => (_rnd ?? Random.Shared).NextDouble() * (Max - Min) + Min;
}
