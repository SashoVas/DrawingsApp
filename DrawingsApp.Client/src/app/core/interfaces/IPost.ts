export interface IPost{
    id:number,
    imgUrls:Array<string>,
    postedOn:string,
    title:string,
    senderUserName:string,
    groupName:string,
    groupId:number,
    likes:number
}