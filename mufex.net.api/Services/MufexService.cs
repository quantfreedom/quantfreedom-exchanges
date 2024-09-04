namespace mufex.net.api.Services;

public class MufexService
{
    private readonly string? apiKey;
    private readonly string? apiSecret;
    private readonly string? baseUrl;
    private readonly bool debugMode;
    public readonly MufexApiService apiService;
    public readonly MufexPlacersService placers;
    public readonly MufexGettersService getters;
    public readonly MufexSettersService setters;

    public readonly MufexCancelersService cancelers;

    public MufexService(
        string? apiKey = null,
        string? apiSecret = null,
        string? baseUrl = null,
        bool debugMode = false
    )
    {
        this.apiKey = apiKey;
        this.apiSecret = apiSecret;
        this.baseUrl = baseUrl;
        this.debugMode = debugMode;

        this.apiService = new MufexApiService(
            apiKey: apiKey,
            apiSecret: apiSecret,
            baseUrl: baseUrl,
            debugMode: debugMode
        );

        this.placers = new MufexPlacersService(
            apiService: this.apiService
        );

        this.getters = new MufexGettersService(
            apiService: this.apiService
        );
        this.setters = new MufexSettersService(
            apiService: this.apiService
        );
        this.cancelers = new MufexCancelersService(
            apiService: this.apiService
        );
    }
}
