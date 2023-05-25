namespace Danish.Holidays;

[Flags]
public enum HolidayType
{
    None = 0,
    Juleaftensdag = 1,
    FoersteJuledag = Juleaftensdag << 1,
    AndenJuledag = Juleaftensdag << 2,
    Nytaarsaften = Juleaftensdag << 3,
    Nytaarsdag = Juleaftensdag << 4,
    SkaerTorsdag = Juleaftensdag << 5,
    LangFredag = Juleaftensdag << 6,
    PaaskeSoendag = Juleaftensdag << 7,
    AndenPaaskedag = Juleaftensdag << 8,
    /// <summary>
    /// Removed from 2024 and until ?? 
    /// </summary>
    StoreBededag = Juleaftensdag << 9,
    KristiHimmelfartsdag = Juleaftensdag << 10,
    PinseSoendag = Juleaftensdag << 11,
    AndenPinsedag = Juleaftensdag << 12,
    Grundlovsdag = Juleaftensdag << 13,
    FoersteMaj = Juleaftensdag << 14,

    ALL = ~(-1 << 15)
}

