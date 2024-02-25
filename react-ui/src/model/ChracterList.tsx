import { Character } from "./Model";
import BlankCharacter from "../BlankCharacter.json"
import { Guid } from "guid-typescript"

const character_list_alias = "character_list";

export function characterStore(character: Character, prev_guid?: Guid) {
    const character_list = localStorage.getItem(character_list_alias);
    if (!character_list) {
        localStorage.setItem(character_list_alias, JSON.stringify([character]));
    }
    else {
        let lst = JSON.parse(character_list) as Character[];
        const idx = lst.findIndex((c) => c.guid == (prev_guid || character.guid));
        if (idx == -1)
            lst.push(character);
        else
            lst[idx] = character;
        localStorage.setItem(character_list_alias, JSON.stringify(lst));
    }

    return true;
}

export function getBlankCharacter(guid?: Guid) {
    let c = JSON.parse(JSON.stringify(BlankCharacter)) as Character;
    c.guid = guid || Guid.create();
    return c;
}

export function characterGetAny() {
    const character_list = localStorage.getItem(character_list_alias);
    if (!character_list) {
        const chr = getBlankCharacter();
        characterStore(chr);
        return chr;
    }
    else {
        return (JSON.parse(character_list) as Character[])[0];
    }
}

export function characterGetById(guid: Guid) {
    const character_list = localStorage.getItem(character_list_alias);
    if (!character_list) {
        return undefined;
    }
    else {
        const list = JSON.parse(character_list) as Character[];
        return list.find(c => c.guid == guid);
    }
}


export function characterGetAll() {
    const character_list = localStorage.getItem(character_list_alias);
    if (!character_list) {
        const chr = getBlankCharacter();
        characterStore(chr);
        return [chr];
    }
    else {
        return (JSON.parse(character_list) as Character[]);
    }
}

export function characterCreate() {
    const character_list = localStorage.getItem(character_list_alias);
    let new_chr = getBlankCharacter();
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

export function characterRemove(guid: Guid) {
    const character_list = localStorage.getItem(character_list_alias);
    if (character_list) {
        let lst = JSON.parse(character_list) as Character[];
        lst = lst.filter((c) => c.guid != guid);
        localStorage.setItem(character_list_alias, JSON.stringify(lst));
    }
}
