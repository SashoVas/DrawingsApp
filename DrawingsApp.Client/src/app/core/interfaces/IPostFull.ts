import { IComment } from "./IComment";

export interface IPostFull{
    id:string,
    imgUrls:Array<string>,
    description:string,
    postedOn:string,
    title:string,
    likes:number,
    senderName:string,
    groupName:string,
    groupId:number,
    role:number,
    comments:Array<IComment>
}