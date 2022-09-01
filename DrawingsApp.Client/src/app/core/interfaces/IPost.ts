export interface IPost{
    id:string,
    imgUrls:Array<string>,
    postedOn:string,
    title:string,
    senderUserName:string,
    senderId:string,
    groupName:string,
    groupId:number,
    likes:number,
    groupImgUrl:string
}