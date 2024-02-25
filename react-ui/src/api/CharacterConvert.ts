import { Guid } from "guid-typescript";
import { Character, CharacterScreen } from "../model/Model";
import { ApiCharacter } from "./ApiCharacter";

export function characterModelToApi(c: Character) {
	const ch: ApiCharacter = {
		character_id: c.guid.toString(),
		name: c.name,
		max_health: c.max_health,
		health: c.health,
		level: c.level,
		armor_class: c.armor_class,
		attributes: c.attributes,
		skills: c.skills  // reference copy, may be dangerous, check later
    }

    return ch;
}

export function characterComposeFromApi(c: ApiCharacter, s: CharacterScreen[]) {
    const ch: Character = {
		guid: Guid.parse(c.character_id),
		name: c.name,
		max_health: c.max_health,
		health: c.health,
		level: c.level,
		armor_class: c.armor_class,
		attributes: c.attributes,
		skills: c.skills,  // reference copy, may be dangerous, check later

        stored_on_server: true,
        screens: s,
    }

    return ch;
}
