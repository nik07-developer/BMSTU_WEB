import { Card, Paper, TextField, Typography, useTheme } from "@mui/material";
import { ChangeEvent } from "react";
import { attributeDisplayName, attributeModifier, CharacterData } from "../../@types/Model"

function WxAttributes(data: CharacterData, setData: (data: CharacterData) => void) {
	const theme = useTheme();
	const onAttrChange = (ev: any, attr_name: string) => {
		let new_attributes = [...data.attributes];
		const idx = new_attributes.findIndex((e) => e.name == attr_name);
		new_attributes[idx].value = ev.target.value;
		setData({...data, attributes: new_attributes });
	}
	return (
		<Paper elevation={0} sx={{ p: 0.25, display: "flex", flexDirection: "row", flexBasis: 2}}>
			{data.attributes.map((attr) => {
				return (
					<Card sx={{ m: 0.25, width:"50px"}} elevation={2}>
						<Typography textAlign="center">
							{attributeDisplayName(attr.name)[1].toUpperCase()}
						</Typography>
						<Typography textAlign="center" sx={{bgcolor: theme.palette.action.disabledBackground}}>
							<TextField sx={{p: 0.5}} variant="standard" type="number"
								defaultValue={attr.value} onChange={(e) => onAttrChange(e, attr.name)}/>
						</Typography>
						<Typography textAlign="center">
							{(attributeModifier(attr.value) > 0 ? "+" : "") + attributeModifier(attr.value).toString()}
						</Typography>
					</Card>);
			})}
		</Paper>
	);
}

export default WxAttributes;