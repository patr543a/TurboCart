using Session = TurboCart.Domain.Entities.TurboCartSession;

namespace TurboCart.Presentation.Websites.TurboCartSession.Components.Pages;

public partial class Home
{
	private Session? _session;

	protected override void OnInitialized()
	{
		SessionService.OnSessionChanged(session => 
		{
			_session = session;

			var list = _session.Carts.ToList();

			list.Sort((c1, c2) =>
				c1.IsFinished && c2.IsFinished ? TimeSpan.Compare(c1.LapTime, c2.LapTime) :
				c1.IsFinished && !c2.IsFinished ? -1 :
				!c1.IsFinished && c2.IsFinished ? 1 :
				c1.LapNumber != c2.LapNumber ?
				c2.LapNumber.CompareTo(c1.LapNumber) :
				TimeSpan.Compare(c2.LapTime, c1.LapTime));

			_session.Carts = list;

			InvokeAsync(StateHasChanged);
		});

		SessionService.Connect();
	}
}
