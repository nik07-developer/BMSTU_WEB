import { Card, Paper, TextField, Typography, useTheme } from "@mui/material";
import { attributeDisplayName, attributeModifier, Character, CharacterAttributes, characterClone } from "../../model/Model"

function WxAttributes(character: Character, setCharacter: (c: Character) => void, editMode: boolean) {
	const theme = useTheme();
	const onAttrChange = (value: number, attr_key: keyof CharacterAttributes) => {
		let chr = characterClone(character);
		chr.attributes[attr_key].value = value;
		setCharacter(chr);
	}
	return (
		<Paper elevation={0} sx={{ p: 0.25, display: "flex", flexDirection: "row", flexBasis: 2 }}>
			{Object.keys(character.attributes).map((key_name: string) => {
                const key = key_name as keyof CharacterAttributes;
                const attribute = character.attributes[key];
				return (
					<Card sx={{ m: 0.25, width: "50px" }} elevation={2}>
						<Typography textAlign="center">
							{attributeDisplayName(key_name)[1].toUpperCase()}
						</Typography>
						<Typography textAlign="center" sx={{ bgcolor: theme.palette.action.disabledBackground }}>
							{editMode && (
								<TextField sx={{ p: 0.5 }} variant="standard" type="number"
									defaultValue={attribute.value} onChange={(e) => onAttrChange(Number.parseInt(e.target.value), key)} />)}
							{!editMode && (
								<Typography>
									{attribute.value}
								</Typography>)}
						</Typography>
						<Typography textAlign="center">
							{(attributeModifier(attribute.value) > 0 ? "+" : "") + attributeModifier(attribute.value).toString()}
						</Typography>
					</Card>);
			})}
		</Paper>
	);
}

export default WxAttributes;
