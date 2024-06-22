import { CategoryTypes } from "@/lib/types";
import { ColorTypes } from "@/lib/types";

export function getCategoryType(category: string) {
    switch (category) {
        case 'Straight':
            return CategoryTypes.Straight;
        case 'Even':
            return CategoryTypes.Even;
        case 'Odd':
            return CategoryTypes.Odd;
        case 'Color':
            return CategoryTypes.Color;
        default:
            return null; // Or a default value
    }
}

export function getColorType(color: string) {
    switch (color) {
        case 'Red':
            return ColorTypes.Red;
        case 'Black':
            return ColorTypes.Black;
        default:
            return null; // Or a default value
    }
}