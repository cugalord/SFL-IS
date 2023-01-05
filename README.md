# SFL (Simple Flyway Logistics)
Informacijski sistem za logistično podporo kurirski službi

## Člana Ekipe:
- Luka Šveigl (63200301)
- Nejc Vrčon Zupan (63200327)

## Kazalo
- [Opis delovanja sistema](#opis-delovanja-sistema)
- [Podatkovna baza](#podatkovna-baza)
- [Opis nalog vsakega študenta](#opis-nalog-vsakega-študenta)
- [Spletna aplikacija](#spletna-aplikacija)
- [Mobilna aplikacija](#mobilna-aplikacija)


## Opis delovanja sistema:
Z informacijskim sistemom bova podprla delo zaposlenih v kurirski službi, ki deluje po Sloveniji.
Sistem omogoča check in/check out paketov, pregled nad paketi v skladišču, potrjevanje dostave paketa.
Uporabniki sistema bodo zaposleni v podjetju (skladiščniki, dostavljalci in vodje skladišč).
Informacijski sistem podpira implementacijo oz. nadgradnjo sledenja paketa za stranke.

## Podatkovna baza:
![database](resources/is_model.png)

#### Tabela City
`Tabela City` vsebuje vse poštne številke, kjer lahko opravimo dostavo.
Opis tabele:
- `Stolpec Code` predstavlja poštno številko, ki je tudi primarni ključ v tabeli
- `Stolpec Name` pa predstavlja ime kraja

#### Tabela Street
`Tabela Street` vsebuje točen naslov.
Opis tabele:
- `Stolpec StreetName` predstavlja ime ulice
- `Stolpec StreetNumber` predstavlja ulično številko
- `Stolpec CityCode` (tuji ključ na `tabelo City`) pa predstavlja poštno številko kraja ulice
Primarni ključ sestavljajo vsi trije stolpci.

#### Tabela Branch
`Tabela Branch` vsebuje poslovalnice podjetja.
Opis tabele:
- `Stolpec ID` predstavlja primarni ključ v tabeli
- `Stolpec Name` predstavlja ime poslovalnice
- `Stolpec StreetName` (tuji ključ na tabelo `Street`) predstavlja ime ulice
- `Stolpec StreetNumber` (tuji ključ na tabelo `Street`) predstavlja ulično številko
- `Stolpec CityCode` (tuji ključ na tabelo `Street`) pa predstavlja poštno številko kraja ulice 

#### Tabela StaffRole
`Tabela StaffRole` vsebuje vloge za zaposlene v podjetju.
Opis tabele:
- `Stolpec ID` predstavlja primarni ključ v tabeli.
- `Stolpec Name` predstavlja ime vloge

#### Tabela Staff
`Tabela Staff` vsebuje zaposlene v podjetju.
Opis tabele:
- `Stolpec username` predstavlja hkrati uporabniško ime in primarni ključ v tabeli.
- `Stolpec Name` predstavlja ime zaposlenega
- `Stolpec Surname` predstavlja priimek zaposlenega
- `Stolpec BranchID` (tuji ključ na tabelo `Branch`) predstavlja poslovalnico v kateri je zaposlen uslužbenec.
- `Stolpec RoleID` (tuji ključ na tabelo `StaffRole`) predstavlja vlogo zaposlenega v podjetju.

#### Tabela ParcelStatus
`Tabela ParcelStatus` vsebuje statuse paketa.
Opis tabele:
- `Stolpec ID` predstavlja primarni ključ v tabeli.
- `Stolpec Name` predstavlja ime statusa

#### Tabela Parcel
`Tabela Parcel` vsebuje pakete v sistemu.
Opis tabele:
- `Stolpec ID` predstavlja hkrati šifro paketa in primarni ključ v tabeli
- `Stolpec Weight` predstavlja težo paketa (kg)
- `Stolpec Height` predstavlja višino paketa (cm)
- `Stolpec Width` predstavlja širino paketa (cm)
- `Stolpec Depth` predstavlja globino paketa (cm)
- `Stolpec ParcelStatusID` (tuji ključ na tabelo `ParcelStatus`) predstavlja trenutni status paketa
- `Stolpec RecipientCode` (tuji ključ na tabelo `Street`) predstavlja prejemnikovo poštno številko
- `Stolpec RecipientStreetName` (tuji ključ na tabelo `Street`) predstavlja prejemnikovo ulico
- `Stolpec RecipientStreetNumber` (tuji ključ na tabelo `Street`) predstavlja prejemnikovo ulično številko
- `Stolpec SenderCode` (tuji ključ na tabelo `Street`) predstavlja pošiljateljevo poštno številko
- `Stolpec SenderStreetName` (tuji ključ na tabelo `Street`) predstavlja pošiljateljevo ulico
- `Stolpec SenderStreetNumber` (tuji ključ na tabelo `Street`) predstavlja pošiljateljevo ulično številko

#### Tabela JobType
`Tabela JobType` vsebuje vse tipe opravil.
Opis tabele:
- `Stolpec ID` predstavlja primarni ključ v tabeli
- `Stolpec Name` predstavlja ime opravila

#### Tabela JobStatus
`Tabela JobStatus` vsebuje vse statuse opravil.
Opis tabele:
- `Stolpec ID` predstavlja primarni ključ v tabeli
- `Stolpec Name` predstavlja ime statusa

#### Tabela JobParcel
`Tabela JobParcel` povezuje opravilo z paketom oz. paketi.
Opis tabele:
- `Stolpec ID` predstavlja primarni ključ v tabeli
- `Stolpec ParcelID` (tuji ključ na tabelo `Parcel`) predstavlja šifro paketa
- `Stolpec JobID` (tuji ključ na tabelo`Job`) predstavlja ID opravila

#### Tabela Job
`Tabela Job` vsebuje opravila v sistemu.
Opis tabele:
- `Stolpec ID` predstavlja primarni ključ v tabeli
- `Stolpec DateCreated` predstavlja datum, ko je bilo opravilo kreirano
- `Stolpec DateCompleted` predstavlja datum, ko je bilo opravilo zaključeno
- `Stolpec JobStatusID` (tuji ključ na tabelo `JobStatus`) predstavlja trenutni status opravila
- `Stolpec JobTypeID` (tuji ključ na tabelo `JobType`) predstavlja tip opravila
- `Stolpec StaffUsername` (tuji ključ na tabelo `Staff`) določa komu je opravilo namenjeno

#### Sistemske tabele AspNet
`Tabele AspNet` vsebujejo uporabnike v sistemu. Uporabniška imena so enaka v tabeli `Staff`.

## Opis nalog vsakega študenta:
Vsi podatki informacijskega sistema so shranjeni v podatkovni bazi, za povezavo med uporabniškim vmesnikom in podatkovno bazo je uporabljeno .NET razvojno ogrodje, aplikacija pa bo tekla na Azure spletni platformi.

## Spletna aplikacija
Uporabnik se prijavi v aplikacijo z njegovim uporabniškim imenom in geslom.
Uporabnikom se glede na njihovo vlogo v podjetju (uporabniška hierarhija) odprejo razlicna opravila:
- Skaldiščniki in dostavljalci nato vidijo svoja opravila, ki jih morajo opraviti.
- Vodja skladišča lahko vidi še pregled nad celotnim skladiščem (kje je zaposlen) in zgodovini paketa.
- Logistik ima pregled nad vsemi skladišči in paketi, prav tako pa potrjuje naročila paketov
- Administrator pa lahko ima poln dostop nad celotnim informacijskim sistemom

## Mobilna aplikacija