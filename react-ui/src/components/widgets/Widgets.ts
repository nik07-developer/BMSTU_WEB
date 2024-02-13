import WxArmor from "./WxArmor";
import WxAttributes from "./WxAttributes";
import WxHealth from "./WxHealth";
import WxLevel from "./WxLevel";
import WxSkills from "./WxSkills";
import WxUnknown from "./WxUnknown";

export function htmlByWxName(name: string) {
	switch (name) {
		case "attributes-view":
			return WxAttributes;
		case "health-view":
			return WxHealth;
		case "armor-view":
			return WxArmor;
		case "skills-view":
			return WxSkills;
		case "level-view":
			return WxLevel;
	}

    return WxUnknown;
}
