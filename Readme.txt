Legality Checker vB54-24 - February 18, 2010
Author: Sabresite

Prerequisites:
- Microsoft .NET Framework 2.0 SP1 - http://msdn.microsoft.com/en-us/netframework/aa731542.aspx
- Microsoft Windows 2000/2003/XP/Vista/2008/7

Installation:
- Extract to a folder.

Usage:
- Drop PKM file(s) onto the executable.
- Drop folder(s) onto the executable.

What does this program do?
Legality Checker is a simple tool designed to aid a person in determining if their Diamond/Pearl/Platinum Pokemon is Legal.
The program checks for certain values and relationships which cannot be seen in-game.  Everything else should be viewable in-game.

CheckSum:
- This number represents a crude integrity check of the pokemon data.  If it is Invalid, the pokemon was modified externally from the game.

Pokemon ID:
- This number is the personality ID of the pokemon.  It determines nature, gender, ability, and shinyness
- The ability (1 or 2), should correspond to the ability seen in-game.  This may be different for pal parked pokemon where abilities were
later introduced for Diamond/Pearl.

Individual Values (IVs):
- This is a set of numbers representing the pokemon's genetic disposition to a particular stat.  The set is in order of:
- HP, Attack, Defense, Speed, Special Attack, Special Defense

Secret ID:
- This number distinguishes a trainer from others which may have the same name and/or trainer id.

Hidden Power:
- The type and damage related to the Hidden Power move.

Fateful Encounter:
- Certain pokemon from promotional events (like mystery gifts) have this flag.

Gender Check:
- If this is invalid, the pokemon is hacked.

Effort Values:
- If this is invalid, the pokemon was altered by an external program.

Trash Bytes:
- Pokemon Pal Parked from GBA have this.  If this is displayed, it must be valid or the pokemon is hacked.
  - Pokemon which have evolved after they were pal parked will have invalid trash bytes.
- Mystery Gift pokemon also have this.  If this is displayed, it must be valid or the pokemon is hacked.

Country Originated:
- This is the country/language that the pokemon originated from.
- For pal parked pokemon, it may be different then the Pal Parked Country.

Pal Parked Country:
- This is the country of the diamond/pearl game that pal parked the pokemon.
- This is determined by trash bytes.  If trash bytes are invalid, this does not apply (since the pokemon is hacked anyways).
- If the pal park country says "Indeterminate", this is because there are no trash bytes to check.
- If a pokemon evolved, the trash bytes may say "Japan" or "Invalid" instead of the proper country.  This is normal.
- A pokemon that was pal parked by a different country than the country of origin or the korean game must be nicknamed.
  In this scenario, the trash bytes will be invalid if the pokemon does not have the IsNicknamed flag.

Sync:
- This refers only to wild encountered 4th generation and in-game legend Pokemon.  This does not include the great marsh,
  gift, starter, in-game trade, special acquisition, or roaming Pokemon.
- If this is Invalid, then the Pokemon is hacked.

Type:
- Normal NDS or GBA - This type is the most common and will be seen on wild/legend encountered pokemon in nds/gba games.
- Wild GBA - This type is less common and will be seen on wild encountered pokemon in gba games.
  - It is unclear whether GBA legends will be seen with this type.
- Uncommon GBA - This type is seen uncommonly by wild encountered pokemon in gba games.
- Rare GBA - This type is seen rarely by wild encountered pokemon in gba games.
- Very Rare GBA - This type is seen in less than 1% of wild encountered pokemon in gba games.
- Ultra Rare GBA - This type is seen in less than 0.01% of wild encountered pokemon in gba games.
- Common GBA Event (Restricted) - This type is seen with particular promotional GBA pokemon.
  The following pokemon should ALWAYS be this type.
  - WISHMKR Jirachi
  - 10th Anniversary Pokemon (All countries)
  - Bryant Park 10 ANIV Pokemon
  - SPACE C Deoxys 
  - DOEL Deoxys
  - ROCKS & Festa Metang
  - GW, Yokohama & ANA pikachu
  - Mitsurin Celebi
  - Sunday Pikachu and Wobbuffet
  - PCJP Pokemon
  - Pokepark Pokemon
  - Hadou Mew and Regis
  - Tanabata Jirachi (2004-2006)
  - Aura Mew (CANNOT BE SHINY)
- Common GBA Event (Unrestricted) - This type is seen with particular promotional GBA pokemon.
  The following pokemon use both restricted & unresticted:
  - Pokemon Center Japan 
  - Negai Boshi Jirachi
  - Pokemon Box pichu, skitty, swablu, and zigzagoon
  - MYSTRY mew
  - There may be more.  Please report confirmed pokemon events so they can be added to this list.

More Types:
- Egg - This is a pokemon in an egg.  There is not much to check.
- Hatched - This pokemon was hatched from an egg.  Not much to check.
- Honey Tree Munchlax - This is a legal Munchlax that came from a Honey Tree.
- Mystery Gift (Static PID) - This is a mystery gift with a static pokemon id.
- Mystery Gift (Dynamic PID) - This is a mystery gift with a dynamic pokemon id.  
  Make sure to check it thoroughly with another program to be sure its legit.
- Hacked Mystery Gift - The mystery gift is a hacked static pid mystery gift.
- Egg from Manaphy Event - This is an egg from the pokemon ranger manaphy event.
- Static (usually in-game traded) - This is a static pokemon, usually it was traded from an in-game source. 
- Hatched from Manaphy Event - This is a manaphy hatched from the pokemon ranger event.
- Hacked Manaphy Event - This is a hacked manaphy.
- Valid USA (or Japan) Berry Glitch Zigzagoon (RUBY or SAPHIRE)
  - This is a legal zigzagoon acquired from the japanese or american demo disc.
  - The name displayed, RUBY or SAPHIRE is based on the trainer's gender, and must match the OT Name.
- Hacked Berry Glitch Zigzagoon
  - This is a hacked zigzagoon, which failed either the OTG, TID, SID, Shiny, PID, IV, Location, HomeTown, or other check.
- Unknown GBA Pokemon - Hacked & Hatched pokemon from GBA will show up as unknown.  Pokemon from XD/Collo will also be unknown.
  These promotional pokemon also fall here:
  - CHANNEL Jirachi
  - Ageto Celebi
  - Koroshiamu pikachu
  - Ruby/Saphire Shiny Zigzagoon
  - PCNY pokemon
- Hacked Pokemon - This pokemon was hacked.


ChangeLog:
Beta 54-24 02/18/2010
- Fixed a bug with Japanese Slot 1 Pal parking D/P
- Fixed a bug with certain NoK pokemon not having the proper distribution region checks.
Beta 54-23 02/07/2010
- Added Pikachu colored Pichu (USA) - THanks Nigoli, Person, Kelly, and others
- Added Jirachi (USA) - Thanks Nigoli, Person, Kelly, and others
- Added 10th Aniv HGSS Mew (53b) - Thanks Nigoli
- Added Unown GBA (Fr/Lg) Algorithm - Thanks DarkDragonite
- Added X-Act Method 3 (A-C-D-E, this is theoretical until I see one)
- Cleaned up NDS and GBA validation so there is no confusion.
- Removed A-B-D-E from NDS.
- Restricted 10th Aniv HGSS Mew (53a) to HGSS only.
Beta 54-22 12/26/2009
- Cleared up the source code for basic legality checking
- Added a wider support for GBA legality
Beta 54-21 12/21/2009
- Fixed various bugs with mystery gifts
- Fixed a bug with ARIO Pikachu support
- Added support for in-game location
Beta 54-20 12/20/2009
- Fixed a bug with all generation 4 mystery gifts
- Added ARIO Pikachu
- Added 10th Anniv Mew
- Fixed a bug with USA Platinum Trash Bytes
Beta 54-19 12/10/2009
- Fixed a bug with trash bytes on Non-Jap First slot Pokemon.
Beta 54-18 11/08/2009
- Added Big Brother Slugma egg
- Added AUS/USA Arceus
- Fixed various small bugs
Beta 54-17 10/12/2009
- Added all JEREMY pokes
- Added OTG checks to mystery gifts and static pokes
- Fixed various bugs with evo checks and hometown checks
Beta 54-16 10/12/2009
- Added all Diamond/Pearl/Platinum in-game traded pokes
- Added all Emerald in-game traded pokes
- Added various HG/SS in-game traded and static pokes
- Fixed a bug with mystery gift evolution checks
Beta 54-15 10/10/2009
- Fixed a bug with world distribution mystery gifts
- Added display of acquisition date
Beta 54-14 10/10/2009
- Added PikaCafes (I missed them?)
- Added Release Date checks for mystery gifts
- Added Language checks for mystery gifts
- Added Hometown checks for mystery gifts
- Added Evolution support for mystery gifts
- Fixed various bugs with mystery gifts
Beta 54-12 10/08/2009
- Added "Big Brother" Mareep Egg
- Added 2009 CMEX Kyoto Pikachu
- Added 2009 Birthday Chimchar 2009
Beta 54-11 09/20/2009
- Added French Regigigas
- Added Sync check to Chain Shinies (may not apply in all cases though)
- Added Berry Glitch Shiny Zigzagoon Check #1
  - Thanks to Invader Tak, Godzilla, & McFizzle for their contributions.
Beta 54-10 09/09/2009
- Added Synchronize validity thanks to mingot
Beta 54-9 09/06/2009
- Added VGC 2009 Milotic
- Added European (Spanish/Italian) Regigigas
- Added all current Korean Mystery Gifts
- Added Birthday Charmander 2009
Beta 54-8 07/18/2009
- Added Movie 12 Arceus
Beta 54-7 07/10/2009
- Added Notched Pichu
- Added NZ Jirachi
- Added Platinum Trash Bytes (relaxed)
Beta 54-6 05/29/2009
- Added USA Shiny Milotic
Beta 54-5 05/23/2009
- Added French/German Movie 11 Shaymin
Beta 54-4 04/16/2009
- Added UK Movie 11 Shaymin
- Added Spain Movie 11 Shaymin
- Added Osaka Meowth
- Added NoK Mew
- Added another check for all mystery gifts
Beta 54-3 03/02/2009
- Added Honey tree Munchlax legality check thanks to SCV.
- Added ONEMURI Pikachu to the Mystery Gift List.
Beta 54-2 02/02/2009
- Fixed two bugs with genders and the gender check
- Fixed a bug with mystery gifts
- Added all mystery gifts including USA TRU Shaymin and Regigigas
- Added a placeholder for unrestricted dynamic mystery gifts
- Added code to support vB55-G - Almost there!
Beta 54 - 12/8/2008
- Fixed a bug with the Manaphy anti-shiny algorithm.
Beta 53 - 12/6/2008
- Fixed a bug with determining Manaphy legality.
- Added fateful encounter check to the manaphy legality check.
Beta 52 - 12/6/2008
- Found a flaw in GameFreak's code allowing a shiny Manaphy to be possible.  This is possible by hatching someone else's manaphy egg.
- Manaphy eggs conform to Normal NDS algorithm.
- Hatched Manaphy from pokemon ranger conform to Normal NDS (both shiny and nonshiny), or antishiny.
- Fixed a bug with genderless not showing as a proper gender.
Beta 51 - 12/3/2008
- Fixed the file/directory recursion problem when using more than 20 files.
- Fixed a bug with pokemon that have no name terminator.
- Fixed a bug with pokemon that have no original trainer name terminator.
- Added pal park country "Indeterminate"

Download Location:
http://www.projectpokemon.org/main/mirrors/Legal.zip