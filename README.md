# SFL_IS

## SFL (Simple Flyway Logistics)
Informacijski sistem za logistično podporo kurirski službi

#### Člana Ekipe:
- Luka Šveigl (63200301)
- Nejc Vrčon Zupan (63200327)


### Opis problemske domene:
Z informacijskim sistemom bova podprla delo zaposlenih v kurirski službi, ki deluje po Sloveniji.
Sistem omogoča check in/check out paketov, pregled nad paketi v skladišču, potrjevanje dostave paketa.
Uporabniki sistema bodo zaposleni v podjetju (skladiščniki, dostavljalci in vodje skladišč).
Informacijski sistem podpira implementacijo oz. nadgradnjo sledenja paketa za stranke.

### Opis aplikacije:
Vsi podatki informacijskega sistema so shranjeni v podatkovni bazi, za povezavo med uporabniškim vmesnikom in podatkovno bazo je uporabljeno .NET razvojno ogrodje, aplikacija pa bo tekla na Azure spletni platformi.

### Podatkovna baza:
![database](resources/is_model.png)

### Spletna aplikacija
Uporabnik se prijavi v aplikacijo z njegovim uporabniškim imenom in geslom.
Uporabnikom se glede na njihovo vlogo v podjetju (uporabniška hierarhija) odprejo razlicna opravila:
- Skaldiščniki in dostavljalci nato vidijo svoja opravila, ki jih morajo opraviti.
- Vodja skladišča lahko vidi še pregled nad celotnim skladiščem (kje je zaposlen) in zgodovini paketa.
- Logistik ima pregled nad vsemi skladišči in paketi, prav tako pa potrjuje naročila paketov
- Administrator pa lahko ima poln dostop nad celotnim informacijskim sistemom