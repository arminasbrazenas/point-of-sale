using PointOfSale.DataAccess.Shared.Interfaces;

namespace PointOfSale.DataAccess.BusinessManagement.ErrorMessages;

public class RefreshTokenNotFoundErrorMessage : IPointOfSaleErrorMessage
{
    private readonly int _refreshTokenId;

    public RefreshTokenNotFoundErrorMessage(int refreshTokenId)
    {
        _refreshTokenId = refreshTokenId;
    }

    public string En => $"Refresh token with id '{_refreshTokenId}' not found.";
}
