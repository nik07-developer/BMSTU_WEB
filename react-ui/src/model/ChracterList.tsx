import { Character } from "./Model";
import BlankCharacter from "../BlankCharacter.json"

const character_list_alias = "character_list";
const screen_list_alias = "screen_list";

export function characterStore(character: Character, old_name?: string) {
    const character_list = localStorage.getItem(character_list_alias);
    if (!character_list) {
        localStorage.setItem(character_list_alias, JSON.stringify([character]));
    }
    else {
        let lst = JSON.parse(character_list) as Character[];
        const idx = lst.findIndex((c) => c.name == (old_name || character.name));
        const last_idx = lst.findLastIndex((c) => c.name == character.name);
        if (idx == -1) {
            lst.push(character);
        }
        else if (last_idx == -1 || last_idx == idx) {
            lst[idx] = character;
        }
        else {
            return false;
        }
        localStorage.setItem(character_list_alias, JSON.stringify(lst));
    }

    return true;
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

export function characterGetAll() {
    const character_list = localStorage.getItem(character_list_alias);
    if (!character_list) {
        const chr = JSON.parse(JSON.stringify(BlankCharacter)) as Character;
        characterStore(chr);
        return [chr];
    }
    else {
        return (JSON.parse(character_list) as Character[]);
    }
}

export function characterCreate() {
    const character_list = localStorage.getItem(character_list_alias);
    let new_chr = JSON.parse(JSON.stringify(BlankCharacter)) as Character;
    if (!character_list) {
        localStorage.setItem(character_list_alias, JSON.stringify([new_chr]));
    }
    else {
        let lst = JSON.parse(character_list) as Character[];
        let idx = lst.findIndex((c) => c.name == new_chr.name);
        let num = 1;
        while (idx != -1) {
            num += 1;
            idx = lst.findIndex((c) => c.name == (new_chr.name + " (" + num.toString() + ")"));
        }

        if (num > 1)
            new_chr.name += " (" + num.toString() + ")";

        lst.push(new_chr);
        localStorage.setItem(character_list_alias, JSON.stringify(lst));
    }
}

export function characterRemove(name: string) {
    const character_list = localStorage.getItem(character_list_alias);
    if (character_list) {
        let lst = JSON.parse(character_list) as Character[];
        lst = lst.filter((c) => c.name != name);
        localStorage.setItem(character_list_alias, JSON.stringify(lst));
    }
}
