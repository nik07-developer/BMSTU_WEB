import { Character } from "./Model";
import BlankCharacter from "../BlankCharacter.json"

const character_list_alias = "character_list";
const screen_list_alias = "screen_list";

export function characterStore(character: Character) {
    const character_list = localStorage.getItem(character_list_alias);
    if (!character_list) {
        localStorage.setItem(character_list_alias, JSON.stringify([character]));
    }
    else {
        let lst = JSON.parse(character_list) as Character[];
        const idx = lst.findIndex((c) => c.name == character.name);
        if (idx != -1) {
            lst[idx] = character;
        }
        else {
            lst.push(character);
        }
        localStorage.setItem(character_list_alias, JSON.stringify(lst));
    }
}

export function characterLoad(character_name: string) {
    const character_list = localStorage.getItem(character_list_alias);
    if (!character_list)
        return undefined;

    return (JSON.parse(character_list) as Character[]).find((c) => c.name = character_name);
}

export function characterGetAny() {
    const character_list = localStorage.getItem(character_list_alias);
    if (!character_list) {
        const chr = JSON.parse(JSON.stringify(BlankCharacter)) as Character;
        characterStore(chr);
        return chr;
    }
    else {
        return (JSON.parse(character_list) as Character[])[0];
    }
}
