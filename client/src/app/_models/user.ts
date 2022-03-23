// interfejs w TS uzywany jest do okreslenia ze cos jest rodzajem czegos
export interface User {
    username: string;
    token: string;
    photoUrl: string;
    knownAs: string;
    gender: string;
    roles: string[];
}