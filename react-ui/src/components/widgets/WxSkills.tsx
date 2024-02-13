import { Box, Card, Paper, Typography, useTheme } from "@mui/material";
import { attributeDisplayName, attributeModifier, Character, Skill, skillInfo, SkillProficiency } from "../../model/Model"

function WxSkills(character: Character, setCharacter: (c: Character) => void) {
    const skillValue = (skill: Skill) => {
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

        const skill_prof = skillInfo(skill.name)[1];
        prof += attributeModifier((character.attributes.find((el) => el.name == skill_prof) || { name: "unknown", value: 0 }).value);
        return (prof > 0 ? "+" : "") + prof.toString();
    }

	const theme = useTheme();
	return (
		<Paper elevation={0} sx={{ p: 0.25, display: "flex", flexDirection: "column"}}>
			{character.skills.map((skill) => {
				return (
					<Card sx={{ m: 0.25, display: "flex", justifyContent: "space-between"}} elevation={2}>
                        <Box sx={{display: "flex"}}>
                            <Typography textAlign="center" sx={{bgcolor: theme.palette.action.disabledBackground, pr: "5px", pl: "5px", width: "25px"}}>
                                {skill.proficiency}
                            </Typography>
                            <Typography textAlign="left" sx={{pl: "5px", minWidth: "170px"}}>
                                {skillInfo(skill.name)[0]}
                            </Typography>
                        </Box>
                        <Box sx={{display: "flex"}}>
                            <Typography textAlign="center" sx={{bgcolor: theme.palette.action.disabledBackground, pr: "5px", pl: "5px", width: "45px"}}>
                                {attributeDisplayName(skillInfo(skill.name)[1])[1]}
                            </Typography>
                            <Typography textAlign="right" sx={{width: "35px", pr: "5px", pl: "5px"}}>
                                {skillValue(skill)}
                            </Typography>
                        </Box>
					</Card>);
			})}
		</Paper>
	);
}

export default WxSkills;
