import { Box, Card, IconButton, Paper, SvgIcon, Typography, useTheme } from "@mui/material";
import { attributeDisplayName, attributeModifier, Character, CharacterAttribute, CharacterAttributes, characterClone, Skill, skillInfo, SkillProficiency, Skills } from "../../model/Model"
import ButtonProficiency from "../ProficiencyIcon";

function WxSkills(character: Character, setCharacter: (c: Character) => void) {
    const skillValue = (skill_name: string, skill: Skill) => {
        let prof: number;
        switch (skill.proficiency) {
            case SkillProficiency.Untrained:
                prof = 0;
                break;
            case SkillProficiency.Trained:
                prof = Math.floor((character.level - 1) / 4) + 2;
                break;
            case SkillProficiency.Expert:
                prof = 2 * (Math.floor((character.level - 1) / 4) + 2);
                break;
        }

        const associated_attribute = skillInfo(skill_name)[1] as keyof CharacterAttributes;
        prof += attributeModifier((character.attributes[associated_attribute] || { value: 0 }).value);
        return (prof > 0 ? "+" : "") + prof.toString();
    }

    const toggleProficiency = (idx: keyof Skills) => {
        let chr = characterClone(character);
        switch (chr.skills[idx].proficiency) {
            case SkillProficiency.Untrained:
                chr.skills[idx].proficiency = SkillProficiency.Trained;
                break;
            case SkillProficiency.Trained:
                chr.skills[idx].proficiency = SkillProficiency.Expert;
                break;
            case SkillProficiency.Expert:
                chr.skills[idx].proficiency = SkillProficiency.Untrained;
                break;
        }
        setCharacter(chr);
    }

    const theme = useTheme();
    return (
        <Paper elevation={0} sx={{ p: 0.25, display: "flex", flexDirection: "column" }}>
            {Object.keys(character.skills).map((key_name: string) => {
                const key = key_name as keyof Skills;
                const skill = character.skills[key];
                return (
                    <Card sx={{ m: 0.25, display: "flex", justifyContent: "space-between" }} elevation={2}>
                        <Box sx={{ display: "flex" }}>
                            <IconButton sx={{ pr: "5px", pl: "5px", width: "25px", height: "25px" }}
                                onClick={() => toggleProficiency(key)}>
                                {ButtonProficiency(skill.proficiency)}</IconButton>
                            <Typography textAlign="left" sx={{ pl: "5px", minWidth: "170px" }}>
                                {skillInfo(key_name)[0]}
                            </Typography>
                        </Box>
                        <Box sx={{ display: "flex" }}>
                            <Typography textAlign="center" sx={{ bgcolor: theme.palette.action.disabledBackground, pr: "5px", pl: "5px", width: "45px" }}>
                                {attributeDisplayName(skillInfo(key_name)[1])[1]}
                            </Typography>
                            <Typography textAlign="right" sx={{ width: "35px", pr: "5px", pl: "5px" }}>
                                {skillValue(key_name, skill)}
                            </Typography>
                        </Box>
                    </Card>);
            })}
        </Paper>
    );
}

export default WxSkills;
